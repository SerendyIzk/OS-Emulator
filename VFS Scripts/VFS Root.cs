using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class VFSRoot : VFSDirectory
{
	public VFSRoot() : base("root","root",null){}

	public VFSNode?SearchFromRoot(string absPath){
		if(absPath==Name)return this;
		Tokenizer tokenizer=new();
		List<string>pathParts=tokenizer.ParsePath(absPath);
		pathParts.Remove(Name);
		return SearchFromCurrent(String.Join('/',pathParts));}
}
