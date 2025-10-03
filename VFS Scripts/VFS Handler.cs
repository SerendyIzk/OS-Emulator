using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

public class VFSHandler
{
	private XDocument? _vfsXML;
	private XElement? _vfsRootInXML;
	private VFSRoot _vfsRootObj=new();
	public VFSRoot VFSRootObj{get=>_vfsRootObj;private set=>_vfsRootObj=value;}

	public void ProcessXML(string path){
		if(!ValidateXML(path))return;
		_vfsRootObj.BuildTree(_vfsRootInXML);
		EventBus.VFSChanged?.Invoke();}

	private bool ValidateXML(string path){
		if(!File.Exists(path)){
			EventBus.OperationReport?.Invoke(OperationCodes.VFSFileError);
			return false;}
		_vfsXML=XDocument.Load(path);
		if(_vfsXML==null){
			EventBus.OperationReport?.Invoke(OperationCodes.VFSFileError);
			return false;}
		_vfsRootInXML=_vfsXML.Root;
		if(_vfsRootInXML==null){
			EventBus.OperationReport?.Invoke(OperationCodes.VFSFileError);
			return false;}
		return true;}
}

