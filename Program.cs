using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
	public static void Main(string[]args){
		string vfsName=args.Length>0?args[0]:"VFS";
		Console.WriteLine($"OS Emulator started, VFS name: {vfsName}");
		Tokenizer tokenizer=new();
		while(true){
			Console.Write($"{vfsName}> ");
			string?inputLine=Console.ReadLine();
			if(string.IsNullOrWhiteSpace(inputLine))continue;
			List<string>?tokens=tokenizer.Parse(inputLine);
			if(tokens==null||tokens.Count==0)continue;
			string command=tokens[0];
			List<string>commandArgs=tokens.GetRange(1,tokens.Count-1);
			switch(command){
				case"exit":
					Console.WriteLine("OS Emulator stopped");
					return;
				case"cd":
					string end=tokens.Count==1?"without arguments":$"with argument: {tokens[1]}";
					if(tokens.Count>2)Console.WriteLine("cd command can accept only 1 or 0 arguments");
					else Console.WriteLine($"cd was called {end}");
						break;
				case"ls":
					Console.WriteLine("ls was called");
					break;
				default:
					Console.WriteLine($"Unknown command: {command}");
					break;}}}
}