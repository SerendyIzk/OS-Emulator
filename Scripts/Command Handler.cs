using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CommandHandler
{
	public void ProcessCommand(string vfsName,string command,List<string>args,VFSRoot?rootObj,ref VFSNode?currentObj){
		Tokenizer tokenizer=new();
		switch(command){
			case"exit":
				EventBus.OperationReport?.Invoke(OperationCodes.Exit);
				break;
			case"cd":
				if(!ObjectVerify(in rootObj,in currentObj))break;
				if(args.Count>=2){
					EventBus.OperationReport?.Invoke(OperationCodes.OtherFailure);
					break;}
				if(args.Count==0)currentObj=currentObj.HeadObj??currentObj;
				else{
					VFSNode?targetObj=FindInspectedObject(in rootObj,in currentObj,in args,in tokenizer);
					if(!ObjectVerify(in targetObj))break;
					currentObj=targetObj;}
				//EventBus.OperationReport?.Invoke(OperationCodes.Success);
				break;
			case"ls":
				if(!ObjectVerify(in rootObj,in currentObj))break;
				if(args.Count>=2){
					EventBus.OperationReport?.Invoke(OperationCodes.OtherFailure);
					break;}
				VFSNode?inspectableObject=FindInspectedObject(in rootObj,in currentObj,in args,in tokenizer);
				if(!ObjectVerify(in inspectableObject))break;
				if(inspectableObject is VFSFile file_1){
					Console.WriteLine($"<FILE CONTENT>:\t\t\t{file_1.Content}");}
				else{
					foreach(VFSNode node in ((VFSDirectory)inspectableObject).Children){
						string id=node is VFSFile?"<FILE>":"<DIR>";
						Console.WriteLine($"{id}\t\t\tNAME:<{node.Name}>\t\t\tPATH:<{node.AbsPath}>");}}
				//EventBus.OperationReport?.Invoke(OperationCodes.Success);
				break;
			case"pwd":
				if(!ObjectVerify(in rootObj,in currentObj))break;
				Console.WriteLine(currentObj.AbsPath);
				//EventBus.OperationReport?.Invoke(OperationCodes.Success);
				break;
			case"who":
				Console.WriteLine($"Current user: {vfsName}");
				//EventBus.OperationReport?.Invoke(OperationCodes.Success);
				break;
			case"du":
				if(!ObjectVerify(in rootObj,in currentObj))break;
				if(args.Count>=2){
					EventBus.OperationReport?.Invoke(OperationCodes.OtherFailure);
					break;}
				VFSNode?inspectedObj=FindInspectedObject(in rootObj,in currentObj,in args,in tokenizer);
				if(!ObjectVerify(in inspectedObj))break;
				if(inspectedObj is VFSFile file_2){
					Console.WriteLine($"<{file_2.Name}>\t\t\t<DISK USAGE: {CountVFSElemSize(file_2)}>");
					break;}
				foreach(VFSNode node in ((VFSDirectory)inspectedObj).Children){
					Console.WriteLine($"<{node.Name}>\t\t\t<DISK USAGE: {CountVFSElemSize(node)}>");}
				//EventBus.OperationReport?.Invoke(OperationCodes.Success);
				break;
			default:
				EventBus.OperationReport?.Invoke(OperationCodes.UnknownCommand);
				break;}}

	private bool ObjectVerify(ref readonly VFSRoot?rootObj,ref readonly VFSNode?currentObj){
		if(rootObj==null||currentObj==null){
			EventBus.OperationReport?.Invoke(OperationCodes.VFSFileError);
			return false;}
		return true;}

	private bool ObjectVerify(ref readonly VFSNode?node){
		if(node==null){
			EventBus.OperationReport?.Invoke(OperationCodes.OtherFailure);
			return false;}
		return true;}

	private VFSNode?FindInspectedObject(ref readonly VFSRoot rootObj,ref readonly VFSNode currentObj,ref readonly List<string>args,ref readonly Tokenizer tokenizer)
		=>args.Count==0?currentObj:tokenizer.ParsePath(args[0])[0]==rootObj.Name?
			rootObj.SearchFromRoot(args[0].ToLower()):currentObj is VFSDirectory dir_2?dir_2.SearchFromCurrent(args[0].ToLower()):null;

	private int CountVFSElemSize(VFSNode obj){
		if(obj is VFSFile file)return file.Content.Length;
		int size=0;
		foreach(VFSNode node in ((VFSDirectory)obj).Children){size+=CountVFSElemSize(node);}
		return size;}
}