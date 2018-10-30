package md533d397bbc82cd8d4157bbe6a9560296c;


public class Program
	extends md5f54719fab2b5008f890ca4d350c867c1.AndroidGameActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("PongBreak.Program, PongBreak", Program.class, __md_methods);
	}


	public Program ()
	{
		super ();
		if (getClass () == Program.class)
			mono.android.TypeManager.Activate ("PongBreak.Program, PongBreak", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
