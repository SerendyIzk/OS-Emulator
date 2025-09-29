using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class VFSRoot : VFSNode
{
	private List<VFSNode>_children=[];
	public List<VFSNode>Children{get=>_children;private set=>_children=value;}

	public VFSRoot(string name="root",string absPath="root",VFSNode? headObj=null) : base(name,absPath,headObj){}

	public void BuildTree(XElement thisElemInXML){
		Children?.Clear();
		foreach(XElement child in thisElemInXML.Elements()){
			string childNameAttribute=child.Attribute("name").Value;
			string childAbsPath=$"{AbsPath}/{childNameAttribute}";
			if(child.Name=="file")Children.Add(new VFSFile(childNameAttribute,childAbsPath,this,child.Value));
			else if(child.Name=="dir"){
				VFSDirectory childDir=new(childNameAttribute,childAbsPath,this);
				childDir.BuildTree(child);
				Children.Add(childDir);}}}
}
