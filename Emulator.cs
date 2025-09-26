using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Emulator
{
	private static bool _exit=false;

	public static void Main(string[]args){
		EventBus.OperationReport+=OperationReport;
		EventBus.OperationReport+=ExitCheck;
		Tokenizer tokenizer=new();
		CommandHandler handler=new();
		StartScriptHandler startScript=new();
		Executor executor=new();
		string vfsPath=args.Length>0?args[0]:"No transmitted file path";
		string vfsName=args.Length>1?args[1]:"VFS";
		string scriptPath=args.Length>2?args[2]:"No transmitted script path";
		Console.WriteLine($"OS Emulator started");
		Console.WriteLine($"VFS path: {vfsPath}, VFS name: {vfsName}, Starting script path: {scriptPath}");
		if(args.Length>2){
			startScript.ProcessStartScript(scriptPath,vfsName);
			_exit=false;}
		while(true){
			executor.Execute(vfsName,tokenizer,handler);
			if(_exit)break;}
		EventBus.OperationReport-=ExitCheck;
		EventBus.OperationReport-=OperationReport;}

	private static void OperationReport(OperationCodes code){
		switch(code){
			case OperationCodes.Success:
				Console.WriteLine("Successfully accomplished");
				break;
			case OperationCodes.UnknownCommand:
				Console.WriteLine("Unknown command");
				break;
			case OperationCodes.IncorrectQuotMarksPlacement:
				Console.WriteLine("Incorrect quotation marks placement");
				break;
			case OperationCodes.Exit:
				Console.WriteLine("OS Emulator stopped");
				break;
			case OperationCodes.NonExistentPath:
				Console.WriteLine("Non-existent path error");
				break;
			case OperationCodes.OtherFailure:
				Console.WriteLine("Error in command");
				break;}}

	private static void ExitCheck(OperationCodes code){if(code==OperationCodes.Exit)_exit=true;}
}