using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Tokenizer
{
	public List<string>?Parse(string lineToParse, int startParseIndex=0){
		StringBuilder currentToken=new();
		List<string>tokens=[];
		bool inDoubleQuotMark=false;
		bool inSingleQuotMark=false;
		bool incorrectCloseQuotMark=false;
		StructuralTypes lastOpenedQuotation=StructuralTypes.None;
		if(startParseIndex>=lineToParse.Length)throw new Exception("Too much parse index");
		for(int i=startParseIndex;i<lineToParse.Length;i++){
			char symbol=lineToParse[i];
			if(symbol=='"'){
				if(inDoubleQuotMark&&lastOpenedQuotation==StructuralTypes.SingleQuotMark&&inSingleQuotMark){
					incorrectCloseQuotMark=true;
					break;}
				if(!inDoubleQuotMark)lastOpenedQuotation=StructuralTypes.DoubleQuotMark;
				inDoubleQuotMark=!inDoubleQuotMark;
				continue;}
			if(symbol=='\''){
				if(inSingleQuotMark&&lastOpenedQuotation==StructuralTypes.DoubleQuotMark&&inDoubleQuotMark){
					incorrectCloseQuotMark=true;
					break;}
				if(!inSingleQuotMark)lastOpenedQuotation=StructuralTypes.SingleQuotMark;
				inSingleQuotMark=!inSingleQuotMark;
				continue;}
			if(char.IsWhiteSpace(symbol)&&!inSingleQuotMark&&!inDoubleQuotMark){
				if(currentToken.Length>0){
					tokens.Add(currentToken.ToString());
					currentToken.Clear();}
				continue;}
			currentToken.Append(symbol);}
		if(currentToken.Length>0)tokens.Add(currentToken.ToString());
		if(inDoubleQuotMark||inSingleQuotMark||incorrectCloseQuotMark){
			EventBus.OperationReport?.Invoke(OperationCodes.IncorrectQuotMarksPlacement);
			return default;}
		return tokens;}
}

