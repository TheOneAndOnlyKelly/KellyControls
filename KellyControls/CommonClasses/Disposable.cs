using System;
using System.Xml.Serialization;

namespace KellyControls.CommonClasses
{
	/// <summary>
	/// Base class that implements the IDisposable interface
	/// </summary>
	public class Disposable : IDisposable
	{

		#region [ Private Variables ]

		[XmlIgnore()]
		protected bool _disposed = false;

		#endregion [ Private Variables ]

		#region [ Constructors ]

		/// <summary>        
		/// The default Constructor.        
		/// </summary>        
		public Disposable()
		{ }

		#endregion [ Constructors ]

		#region [ Destructors ]

		~Disposable()
		{
			//Execute the code that does the cleanup.
			Dispose(false);
		}

		/// <summary>
		/// Releases all resources used by the System.ComponentModel.Component.
		/// </summary>
		/// <param name="disposing">Indicates whether we are currently in the process of cleaning up resources</param>
		public virtual void Dispose(bool disposing)
		{
			// Exit if we've already cleaned up this object.
			if (_disposed)
				return;

			// Clean up objects here
			if (disposing)
				DisposeChildObjects();

			// Remember that we've executed this code
			_disposed = true;
		}

		/// <summary>
		/// Releases all resources used by the System.ComponentModel.Component.
		/// </summary>
		public void Dispose()
		{
			// Execute the code that does the cleanup.
			Dispose(true);

			// Let the common language runtime know that Finalize doesn't have to be called.
			GC.SuppressFinalize(this);
		}

		#endregion [ Destructors ]

		#region [ Methods ]

		/// <summary>
		/// Clean up all child objects here, unlink all events and dispose
		/// </summary>
		protected virtual void DisposeChildObjects()
		{ }

		#endregion [ Methods ]
	}
}
