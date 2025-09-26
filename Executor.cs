using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Executor
{
	public void Execute(string vfsName,Tokenizer tokenizer,CommandHandler cmdHandler){
		string?inputLine=Console.ReadLine();
		Execute(vfsName,inputLine,tokenizer,cmdHandler);}

	public void Execute(string vfsName,string?inputLine,Tokenizer tokenizer,CommandHandler cmdHandler){
		Console.WriteLine($"{vfsName}>{inputLine}");
		if(string.IsNullOrWhiteSpace(inputLine))return;
		List<string>?tokens=tokenizer.Parse(inputLine);
		if(tokens==null||tokens.Count==0)return;
		string command=tokens[0];
		List<string>commandArgs=tokens.GetRange(1,tokens.Count-1);
		cmdHandler.ProcessCommand(command,commandArgs);}
}

