using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace KellyControls.CommonClasses
{
	public class Palette : IEnumerable<NamedColor>, ICloneable
	{
		#region [ Private Variables ]

		private List<NamedColor> _list = null;

		#endregion [ Private Variables ]

		#region [ Properties ]

		public int Count
		{
			get { return _list.Count; }
		}

		/// <summary>
		/// Overloaded index operator
		/// </summary>
		/// <param name="index">Index of the array to use.</param>
		public NamedColor this[int index]
		{
			get { return _list[index]; }
		}

		#endregion [ Properties ]

		#region [ Constructors ]

		public Palette()
		{
			_list = new List<NamedColor>();
		}

		#endregion [ Constructors ]

		#region [ List Methods ]

		/// <summary>
		/// Adds a new Item
		/// </summary>
		/// <param name="item">Item to add</param>
		/// <exception cref="System.ArgumentNullException">item is null</exception>
		public void Add(NamedColor item)
		{
			if (item == null)
				throw new ArgumentNullException("value is null");

			// Check to see if this color value is already present in the list. If so, then we will not add.
			//var NC = _list.Where(n => n.Color.ToArgb() == item.Color.ToArgb()).FirstOrDefault();
			//if (NC != null)
			//	return;

			if (Where(item.Color) != null)
				return;

			_list.Add(item);
		}

		/// <summary>
		/// Adds the elements of the specified collection to the end of the list.
		/// </summary>
		/// <param name="collection">The collection whose elements should be added to the end of the List.
		/// The collection itself cannot be null, nor can any of the elements therein.</param>
		/// <exception cref="System.ArgumentNullException">collection is null</exception>
		public void AddRange(List<NamedColor> collection)
		{
			if (collection == null)
				throw new ArgumentNullException("collection is null");

			foreach (NamedColor Item in collection)
				Add(Item);
		}

		/// <summary>
		/// Adds the elements of the specified collection to the end of the list.
		/// </summary>
		/// <param name="collection">The collection whose elements should be added to the end of the List.
		/// The collection itself cannot be null, nor can any of the elements therein.</param>
		/// <exception cref="System.ArgumentNullException">collection is null</exception>
		public void AddRange(Palette collection)
		{
			if (collection == null)
				throw new ArgumentNullException("collection is null");

			foreach (NamedColor Item in collection)
				Add(Item);
		}

		/// <summary>
		/// Clears out all the items in the list.
		/// </summary>
		public void Clear()
		{
			_list.Clear();
		}

		/// <summary>
		/// Create a deep clone of this object.
		/// </summary>
		public object Clone()
		{
			Palette MyClone = new Palette();
			foreach (NamedColor Item in this)
				MyClone.Add(new NamedColor(Item));
			return MyClone;
		}

		/// <summary>
		/// Determines whether an element is in the list.
		/// </summary>
		/// <param name="item">The item to locate in the list.</param>
		/// <returns>true if item is found in the list; otherwise, false.</returns>
		/// <exception cref="System.ArgumentNullException">Item is null</exception>
		public bool Contains(NamedColor item)
		{
			if (item == null)
				throw new ArgumentNullException("item is null");
			return _list.Contains(item);
		}

		/// <summary>
		/// Searches for the specified item and returns the zero-based index of the first occurrence within the entire list.
		/// </summary>
		/// <param name="item">The item to locate in the list. The value cannot be null.</param>
		/// <exception cref="System.ArgumentNullException">Item is null</exception>
		/// <returns>The zero-based index of the first occurrence of item within the entire list, if found; otherwise, –1.</returns>
		public int IndexOf(NamedColor item)
		{
			if (item == null)
				throw new ArgumentNullException("item is null");
			return _list.IndexOf(item);
		}

		/// <summary>
		/// Inserts an Item into the list at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which item should be inserted.</param>
		/// <param name="item">Item to Insert.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.-or-index is greater than Count.</exception>
		/// <exception cref="System.ArgumentNullException">Item is null</exception>
		public virtual void Insert(int index, NamedColor item)
		{
			if ((index < 0) || (index >= _list.Count))
				throw new ArgumentOutOfRangeException();
			if (item == null)
				throw new ArgumentNullException("item is null");

			_list.Insert(index, item);
		}

		/// <summary>
		/// Removes the first occurrence of a specific Item from the list.
		/// </summary>
		/// <param name="item">The Item to remove from the list. The value cannot be null.</param>
		/// <returns>true if item is successfully removed; otherwise, false. This method also returns false if the item was not found in the list.</returns>
		public bool Remove(NamedColor item)
		{
			if (item == null)
				throw new ArgumentNullException("item is null");

			return _list.Remove(item);
		}

		/// <summary>
		/// Removes the item at the specified index of the list.
		/// </summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">index is less than 0.-or-index is equal to or greater than list.Count.</exception>
		public void RemoveAt(int index)
		{
			if ((index < 0) || (index >= _list.Count))
				throw new ArgumentOutOfRangeException();
			_list.RemoveAt(index);
		}

		public NamedColor Where(Color color)
		{
			foreach (NamedColor nColor in _list)
				if (nColor.Color.Equals(color))
					return nColor;
			return null;
		}

		#endregion [ List Methods ]

		#region [ IEnumerable ]

		/// <summary>
		/// Allows for "foreach" statements to be used on an instance of this class, to loop through all the Channels.
		/// </summary>
		/// <returns></returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator)GetEnumerator();
		}

		public NamedColorEnum GetEnumerator()
		{
			return new NamedColorEnum(_list);
		}

		IEnumerator<NamedColor> IEnumerable<NamedColor>.GetEnumerator()
		{
			return (IEnumerator<NamedColor>)GetEnumerator();
		}

		#endregion [ IEnumerable ]
	}
}
