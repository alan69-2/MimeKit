//
// MessageIdList.cs
//
// Author: Jeffrey Stedfast <jeff@xamarin.com>
//
// Copyright (c) 2013 Jeffrey Stedfast
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using MimeKit.Utils;

namespace MimeKit {
	/// <summary>
	/// A list of Message-Ids, as found in the References header.
	/// </summary>
	public sealed class MessageIdList : IList<string>
	{
		readonly List<string> references;

		/// <summary>
		/// Initializes a new instance of the <see cref="MimeKit.MessageIdList"/> class.
		/// </summary>
		public MessageIdList ()
		{
			references = new List<string> ();
		}

		#region IList implementation

		/// <summary>
		/// Gets the index of the requested messageId, if it exists.
		/// </summary>
		/// <returns>The index of the requested messageId; otherwise <value>-1</value>.</returns>
		/// <param name="messageId">The message-id.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="messageId"/> is <c>null</c>.
		/// </exception>
		public int IndexOf (string messageId)
		{
			if (messageId == null)
				throw new ArgumentNullException ("messageId");

			return references.IndexOf (messageId);
		}

		/// <summary>
		/// Insert the messageId at the specified index.
		/// </summary>
		/// <param name="index">The index to insert the messageId.</param>
		/// <param name="messageId">The message-id to insert.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="messageId"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// <paramref name="index"/> is out of range.
		/// </exception>
		public void Insert (int index, string messageId)
		{
			if (messageId == null)
				throw new ArgumentNullException ("messageId");

			references.Insert (index, messageId);
			OnChanged ();
		}

		/// <summary>
		/// Removes the messageId at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// <paramref name="index"/> is out of range.
		/// </exception>
		public void RemoveAt (int index)
		{
			references.RemoveAt (index);
			OnChanged ();
		}

		/// <summary>
		/// Gets or sets the <see cref="MimeKit.MessageIdList"/> at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="value"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// <paramref name="index"/> is out of range.
		/// </exception>
		public string this [int index] {
			get { return references[index]; }
			set {
				if (value == null)
					throw new ArgumentNullException ("value");

				if (references[index] == value)
					return;

				references[index] = value;
				OnChanged ();
			}
		}

		#endregion

		#region ICollection implementation

		/// <summary>
		/// Add the specified messageId.
		/// </summary>
		/// <param name="messageId">The message-id.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="messageId"/> is <c>null</c>.
		/// </exception>
		public void Add (string messageId)
		{
			if (messageId == null)
				throw new ArgumentNullException ("messageId");

			references.Add (messageId);
			OnChanged ();
		}

		/// <summary>
		/// Clears the messageId list.
		/// </summary>
		public void Clear ()
		{
			references.Clear ();
			OnChanged ();
		}

		/// <summary>
		/// Checks if the <see cref="MessageIdList"/> contains the specified messageId.
		/// </summary>
		/// <returns><value>true</value> if the specified messageId is contained;
		/// otherwise <value>false</value>.</returns>
		/// <param name="messageId">The message-id.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="messageId"/> is <c>null</c>.
		/// </exception>
		public bool Contains (string messageId)
		{
			if (messageId == null)
				throw new ArgumentNullException ("messageId");

			return references.Contains (messageId);
		}

		/// <summary>
		/// Copies all of the messageIds in the <see cref="MimeKit.MessageIdList"/> to the specified array.
		/// </summary>
		/// <param name="array">The array to copy the messageIds to.</param>
		/// <param name="arrayIndex">The index into the array.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="array"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// <paramref name="arrayIndex"/> is out of range.
		/// </exception>
		public void CopyTo (string[] array, int arrayIndex)
		{
			references.CopyTo (array, arrayIndex);
		}

		/// <summary>
		/// Removes the specified messageId.
		/// </summary>
		/// <returns><value>true</value> if the specified messageId was removed;
		/// otherwise <value>false</value>.</returns>
		/// <param name="messageId">The message-id.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="messageId"/> is <c>null</c>.
		/// </exception>
		public bool Remove (string messageId)
		{
			if (messageId == null)
				throw new ArgumentNullException ("messageId");

			if (references.Remove (messageId)) {
				OnChanged ();
				return true;
			}

			return false;
		}

		/// <summary>
		/// Gets the number of headers in the <see cref="MimeKit.MessageIdList"/>.
		/// </summary>
		/// <value>The number of headers.</value>
		public int Count {
			get { return references.Count; }
		}

		/// <summary>
		/// Gets a value indicating whether this instance is read only.
		/// </summary>
		/// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
		public bool IsReadOnly {
			get { return false; }
		}

		#endregion

		#region IEnumerable implementation

		/// <summary>
		/// Gets an enumerator for the list of messageIds.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<string> GetEnumerator ()
		{
			return references.GetEnumerator ();
		}

		#endregion

		#region IEnumerable implementation

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return references.GetEnumerator ();
		}

		#endregion

		/// <summary>
		/// Serializes the <see cref="MimeKit.MessageIdList"/> to a string.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="MimeKit.MessageIdList"/>.</returns>
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();

			for (int i = 0; i < references.Count; i++) {
				if (builder.Length > 0)
					builder.Append (' ');

				builder.Append (references[i]);
			}

			return builder.ToString ();
		}

		internal event EventHandler Changed;

		void OnChanged ()
		{
			if (Changed != null)
				Changed (this, EventArgs.Empty);
		}
	}
}
