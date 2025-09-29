using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class VFSFile : VFSNode
{
	public string Content{get;private set;}

	public VFSFile(string name,string absPath,VFSNode headObj,string content) : base(name,absPath,headObj){Content=content;}
}
