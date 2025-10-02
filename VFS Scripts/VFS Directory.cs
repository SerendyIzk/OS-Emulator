using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class VFSDirectory : VFSNode
{
	private List<VFSNode>_children=[];
	public List<VFSNode>Children{get=>_children;private set=>_children=value;}

	public VFSDirectory(string name,string absPath,VFSNode headObj) : base(name,absPath,headObj){}

	public void BuildTree(XElement thisElemInXML){
		Children?.Clear();
		foreach(XElement child in thisElemInXML.Elements()){
			string childNameAttribute=child.Attribute("name").Value.ToLower();
			string childAbsPath=$"{AbsPath}/{childNameAttribute}";
			if(child.Name=="file")Children.Add(new VFSFile(childNameAttribute,childAbsPath,this,child.Value));
			else if(child.Name=="dir"){
				VFSDirectory childDir=new(childNameAttribute,childAbsPath,this);
				childDir.BuildTree(child);
				Children.Add(childDir);}}}

	public VFSNode?SearchFromCurrent(string relativePath){
		Tokenizer tokenizer=new();
		List<string>pathParts=tokenizer.ParsePath(relativePath);
		VFSDirectory currentObject=this;
		if(pathParts.Count<=0)return null;
		for(int i=0;i<pathParts.Count;i++){
			foreach(VFSNode node in currentObject.Children){
				if(pathParts[i]==node.Name){
					if(i==pathParts.Count-1)return node;
					if(node is VFSDirectory current){
						currentObject=current;
						break;}}}}
		return null;}
}

