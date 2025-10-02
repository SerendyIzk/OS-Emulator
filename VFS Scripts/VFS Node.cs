using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public abstract class VFSNode
{
	public string Name{get;protected set;}
	public string AbsPath{get;protected set;}
	public VFSNode?HeadObj{get;protected set;}

	public VFSNode(string name,string absPath,VFSNode?headObj){
		Name=name;
		AbsPath=absPath;
		HeadObj=headObj;}
}

