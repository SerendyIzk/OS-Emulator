using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Executor
{
	private VFSNode?_currentObj;

	public Executor(VFSNode?currentObj){_currentObj=currentObj;}

	public void Execute(string vfsName,Tokenizer tokenizer,CommandHandler cmdHandler,VFSHandler vfsHandler){
		Console.Write($"{vfsName}>");
		string?inputLine=Console.ReadLine();
		if(string.IsNullOrWhiteSpace(inputLine))return;
		List<string>?tokens=tokenizer.ParseCommand(inputLine);
		if(tokens==null||tokens.Count==0)return;
		string command=tokens[0];
		List<string>commandArgs=tokens.GetRange(1,tokens.Count-1);
		cmdHandler.ProcessCommand(vfsName,vfsHandler,command,commandArgs,vfsHandler.VFSRootObj,ref _currentObj);}

	public void Execute(string vfsName,string?inputLine,Tokenizer tokenizer,CommandHandler cmdHandler,VFSHandler vfsHandler){
		Console.WriteLine($"{vfsName}>{inputLine}");
		if(string.IsNullOrWhiteSpace(inputLine))return;
		List<string>?tokens=tokenizer.ParseCommand(inputLine);
		if(tokens==null||tokens.Count==0)return;
		string command=tokens[0];
		List<string>commandArgs=tokens.GetRange(1,tokens.Count-1);
		cmdHandler.ProcessCommand(vfsName,vfsHandler,command,commandArgs,vfsHandler.VFSRootObj,ref _currentObj);}
}

