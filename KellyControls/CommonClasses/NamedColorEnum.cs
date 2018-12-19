using System;
using System.Collections;
using System.Collections.Generic;

namespace KellyControls.CommonClasses
{
	/// <summary>
	/// NamedColor enumerator class
	/// http://msdn.microsoft.com/en-us/library/system.collections.ienumerable.getenumerator.aspx
	/// </summary>
	public class NamedColorEnum : IEnumerator
	{
		public List<NamedColor> _list;

		// Enumerators are positioned before the first element 
		// until the first MoveNext() call. 
		int position = -1;

		public NamedColorEnum(List<NamedColor> list)
		{
			_list = list;
		}

		public bool MoveNext()
		{
			position++;
			return (position < _list.Count);
		}

		public void Reset()
		{
			position = -1;
		}

		object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		public NamedColor Current
		{
			get
			{
				try
				{
					return _list[position];
				}
				catch (IndexOutOfRangeException)
				{
					throw new InvalidOperationException();
				}
			}
		}
	}
}
