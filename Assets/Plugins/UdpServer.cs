using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class UdpServer {
	[DllImport ("__Internal")]
	private static extern void _init(int port);
	
	[DllImport ("__Internal")]
	private static extern int _startStop();
	
	[DllImport ("__Internal")]
	private static extern string _getMsg();

	public static void init(int port)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor)
			_init(port);
	}
	
	public static int startStop()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor)
			return _startStop();
		else
			return 0;
	}
	
	public static string getMsg()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor)
			return _getMsg();
		else
			return "This must run on an iOS device.";
	}
}
