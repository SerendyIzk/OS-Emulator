using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CommandHandler
{
	public void ProcessCommand(string command,List<string>args){
		switch(command){
			case"exit":
				EventBus.OperationReport?.Invoke(OperationCodes.Exit);
				break;
			case"cd":
				if(args.Count>1){
					EventBus.OperationReport?.Invoke(OperationCodes.OtherFailure);
					break;}
				else{
					EventBus.OperationReport?.Invoke(OperationCodes.Success);
					break;}
			case"ls":
				EventBus.OperationReport?.Invoke(OperationCodes.Success);
				break;
			default:
				EventBus.OperationReport?.Invoke(OperationCodes.UnknownCommand);
				break;}} 
}