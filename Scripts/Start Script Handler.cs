using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartScriptHandler
{
	private bool _exit=false;

	public void ProcessStartScript(string path,string vfsName,VFSHandler vfsHandler){
		if(!File.Exists(path)){
			EventBus.OperationReport?.Invoke(OperationCodes.NonExistentScriptPath);
			return;}
		EventBus.OperationReport+=ExitCheck;
		Tokenizer tokenizer=new();
		CommandHandler handler=new();
		Executor executor=new(vfsHandler.VFSRootObj);
		foreach(string line in File.ReadAllLines(path)){
			executor.Execute(vfsName,line,tokenizer,handler,vfsHandler);
			if(_exit)break;}
		EventBus.OperationReport-=ExitCheck;}

	private void ExitCheck(OperationCodes code){
		switch(code){
			case OperationCodes.Exit:
			case OperationCodes.UnknownCommand:
			case OperationCodes.IncorrectQuotMarksPlacement:
			case OperationCodes.NonExistentScriptPath:
			case OperationCodes.OtherFailure:
			case OperationCodes.VFSFileError:
				_exit=true;
				break;}}
}

