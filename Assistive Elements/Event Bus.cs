using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class EventBus
{
	public static Action<OperationCodes>?OperationReport;
}
