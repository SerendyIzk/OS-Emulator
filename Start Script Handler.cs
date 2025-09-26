using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartScriptHandler
{
	private bool _exit=false;

	public void ProcessStartScript(string path,string vfsName){
		if(!File.Exists(path)){
			EventBus.OperationReport?.Invoke(OperationCodes.NonExistentPath);
			return;}
		EventBus.OperationReport+=ExitCheck;
		Tokenizer tokenizer=new();
		CommandHandler handler=new();
		Executor executor=new();
		foreach(string line in File.ReadAllLines(path)){
			executor.Execute(vfsName,line,tokenizer,handler);
			if(_exit)break;}
		EventBus.OperationReport-=ExitCheck;}

	private void ExitCheck(OperationCodes code){
		switch(code){
			case OperationCodes.Exit:
			case OperationCodes.UnknownCommand:
			case OperationCodes.IncorrectQuotMarksPlacement:
			case OperationCodes.NonExistentPath:
			case OperationCodes.OtherFailure:
				_exit=true;
				break;}}
}

