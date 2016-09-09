package md5c63a200dc84b05203c75e77aeba25bf8;


public class MainActivity_ListWrapper
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("AndroidWeatherApp.MainActivity+ListWrapper, AndroidWeatherApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity_ListWrapper.class, __md_methods);
	}


	public MainActivity_ListWrapper () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity_ListWrapper.class)
			mono.android.TypeManager.Activate ("AndroidWeatherApp.MainActivity+ListWrapper, AndroidWeatherApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
