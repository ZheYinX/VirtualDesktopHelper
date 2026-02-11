/*
 * VirtualDesktopHelper - Unified Interface
 * Part of VirtualDesktopHelper project
 * 
 * Original code by 哲隐
 * 
 * MIT License
 * 
 * Copyright (c) 2026 哲隐
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * DISCLAIMER: This software is provided "as is" without warranty of any kind.
 * The author(s) shall not be liable for any direct, indirect, incidental, 
 * special, exemplary, or consequential damages (including, but not limited to,
 * procurement of substitute goods or services; loss of use, data, or profits;
 * or business interruption) however caused and on any theory of liability,
 * whether in contract, strict liability, or tort (including negligence or 
 * otherwise) arising in any way out of the use of this software, even if 
 * advised of the possibility of such damage.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualDesktopHelper;

namespace VirtualDesktop.Unify
{
	public interface IUnifyVirtualDesktop
	{
		bool MoveWindow(bool direction, bool toDesktop);
		void PinWindow(IntPtr h);
		void UnpinWindow(IntPtr h);
		bool IsPinWindow(IntPtr h);

	}

	public static class UnifyInterfaceManager
	{
		public static IUnifyVirtualDesktop GetInterfaceByOs()
		{
			switch (OSVersionDetector.GetWindowsVersion())
			{
				case WindowsVersion.Win10:
					return new Win10();
				case WindowsVersion.Win11:
					return new Win11();
				case WindowsVersion.Win11_24h2:
					return new Win11_24h2();
				case WindowsVersion.Other:
				default:
					return null;
			}
		}
	}

	public class Win10 : IUnifyVirtualDesktop
	{
		public bool MoveWindow(bool direction, bool toDesktop)
		{
			VirtualDesktop.Win10.Desktop d;
			if (direction)
				d = VirtualDesktop.Win10.Desktop.Current.Left;
			else
				d = VirtualDesktop.Win10.Desktop.Current.Right;

			if (d == null)
				return false;
			d.MoveActiveWindow();
			if (toDesktop)
				d.MakeVisible();
			return true;
		}


		public void PinWindow(IntPtr h)
		{
			VirtualDesktop.Win10.Desktop.PinWindow(h);
		}

		public void UnpinWindow(IntPtr h)
		{
			VirtualDesktop.Win10.Desktop.UnpinWindow(h);
		}

		public bool IsPinWindow(IntPtr h)
		{
			return VirtualDesktop.Win10.Desktop.IsWindowPinned(h);
		}

	}

	public class Win11 : IUnifyVirtualDesktop
	{
		public bool MoveWindow(bool direction, bool toDesktop)
		{
			VirtualDesktop.Win11.Desktop d;
			if (direction)
				d = VirtualDesktop.Win11.Desktop.Current.Left;
			else
				d = VirtualDesktop.Win11.Desktop.Current.Right;

			if (d == null)
				return false;
			d.MoveActiveWindow();
			if (toDesktop)
				d.MakeVisible();
			return true;
		}


		public void PinWindow(IntPtr h)
		{
			VirtualDesktop.Win11.Desktop.PinWindow(h);
		}

		public void UnpinWindow(IntPtr h)
		{
			VirtualDesktop.Win11.Desktop.UnpinWindow(h);
		}

		public bool IsPinWindow(IntPtr h)
		{
			return VirtualDesktop.Win11.Desktop.IsWindowPinned(h);
		}

	}

	public class Win11_24h2 : IUnifyVirtualDesktop
	{
		public bool MoveWindow(bool direction, bool toDesktop)
		{
			VirtualDesktop.Win11_24h2.Desktop d;
			if (direction)
				d = VirtualDesktop.Win11_24h2.Desktop.Current.Left;
			else
				d = VirtualDesktop.Win11_24h2.Desktop.Current.Right;

			if (d == null)
				return false;
			d.MoveActiveWindow();
			if (toDesktop)
				d.MakeVisible();
			return true;
		}

		public void PinWindow(IntPtr h)
		{
			VirtualDesktop.Win11_24h2.Desktop.PinWindow(h);
		}

		public void UnpinWindow(IntPtr h)
		{
			VirtualDesktop.Win11_24h2.Desktop.UnpinWindow(h);
		}

		public bool IsPinWindow(IntPtr h)
		{
			return VirtualDesktop.Win11_24h2.Desktop.IsWindowPinned(h);
		}

	}


}
