using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class VFSDirectory : VFSRoot
{
	public VFSDirectory(string name,string absPath,VFSNode headObj) : base(name,absPath,headObj){}
}

