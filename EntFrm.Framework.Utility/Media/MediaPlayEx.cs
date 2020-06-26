using System;
using System.IO;
using System.Runtime.InteropServices;

namespace EntFrm.Framework.Utility
{
    public class MediaPlayEx
    {
    public enum AudioTrack : byte
		{
			H,
			L,
			R
		}

		public enum Playstate : byte
		{
			Stopped = 1,
			Playing,
			Pause
		}

		public const int MM_MCINOTIFY = 953;

		public const int MCI_NOTIFY_SUCCESS = 1;

		public const int MCI_NOTIFY_SUPERSEDED = 2;

		public const int MCI_NOTIFY_ABORTED = 4;

		public const int MCI_NOTIFY_FAILURE = 8;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		private string ShortPathName = "";

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		private string durLength = "";

		[MarshalAs(UnmanagedType.LPTStr)]
		private string TemStr = "";

		public string SongName;

		public bool Opened;

		private IntPtr WndHandle;

		public bool Mute;

		[DllImport("winmm.dll", CharSet = CharSet.Auto)]
		private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, IntPtr hwndCallback);

		[DllImport("winmm.dll", CharSet = CharSet.Auto)]
		private static extern int mciGetDeviceID(string lpstrName);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetShortPathName(string lpszLongPath, string shortFile, int cchBuffer);

		public int GetDeviceID()
		{
			return MediaPlayEx.mciGetDeviceID("MEDIA");
		}

		public string GetDriverID(string ff)
		{
			ff = ff.ToUpper().Trim();
			string key;
			string result;
			switch (key = ff.Substring(ff.Length - 3))
			{
			case "MID":
				result = "Sequencer";
				return result;
			case "RMI":
				result = "Sequencer";
				return result;
			case "IDI":
				result = "Sequencer";
				return result;
			case "WAV":
				result = "Waveaudio";
				return result;
			case "ASX":
				result = "MPEGVideo2";
				return result;
			case "IVF":
				result = "MPEGVideo2";
				return result;
			case "LSF":
				result = "MPEGVideo2";
				return result;
			case "LSX":
				result = "MPEGVideo2";
				return result;
			case "P2V":
				result = "MPEGVideo2";
				return result;
			case "WAX":
				result = "MPEGVideo2";
				return result;
			case "WVX":
				result = "MPEGVideo2";
				return result;
			case ".WM":
				result = "MPEGVideo2";
				return result;
			case "WMX":
				result = "MPEGVideo2";
				return result;
			case "WMP":
				result = "MPEGVideo2";
				return result;
			case ".RM":
				result = "RealPlay";
				return result;
			case "RAM":
				result = "RealPlay";
				return result;
			case ".RA":
				result = "RealPlay";
				return result;
			case "MVB":
				result = "RealPlay";
				return result;
			}
			result = "MPEGVideo";
			return result;
		}

		public bool OpenMusic(string FileName, IntPtr Handle)
		{
			bool result = false;
			this.CloseMusic();
			this.ShortPathName = "";
			this.ShortPathName = this.ShortPathName.PadLeft(260, Convert.ToChar(" "));
			int num = MediaPlayEx.GetShortPathName(FileName, this.ShortPathName, this.ShortPathName.Length);
			this.ShortPathName = this.GetCurrPath(this.ShortPathName);
			string driverID = this.GetDriverID(this.ShortPathName);
			if (driverID == "RealPlay")
			{
				return false;
			}
			string text = string.Format("open {0} type {1} alias MEDIA ", this.ShortPathName, driverID);
			if (driverID == "AVIVideo" || driverID == "MPEGVideo" || driverID == "MPEGVideo2")
			{
				if (Handle != IntPtr.Zero)
				{
					text += string.Format(" parent {0} style child ", Handle);
				}
				else
				{
					text += " style overlapped ";
				}
			}
			this.TemStr = "";
			this.TemStr = this.TemStr.PadLeft(128, Convert.ToChar(" "));
			num = MediaPlayEx.mciSendString(text, null, 0, IntPtr.Zero);
			MediaPlayEx.mciSendString("set MEDIA time format milliseconds", null, 0, IntPtr.Zero);
			if (num == 0)
			{
				result = true;
				this.Opened = true;
				this.WndHandle = Handle;
				this.SongName = Path.GetFileNameWithoutExtension(FileName);
			}
			return result;
		}

		public bool PlayMusic()
		{
			bool result = false;
			if (MediaPlayEx.mciSendString("play MEDIA notify", null, 0, this.WndHandle) == 0)
			{
				result = true;
			}
			return result;
		}

		public bool SetValume(int Valume)
		{
			bool result = false;
			string lpstrCommand = string.Format("setaudio MEDIA volume to {0}", Valume * 10);
			if (MediaPlayEx.mciSendString(lpstrCommand, null, 0, IntPtr.Zero) == 0)
			{
				result = true;
			}
			return result;
		}

		public bool SetSpeed(int Speed)
		{
			bool result = false;
			string lpstrCommand = string.Format("set MEDIA speed to {0}", Speed);
			if (MediaPlayEx.mciSendString(lpstrCommand, null, 0, IntPtr.Zero) == 0)
			{
				result = true;
			}
			return result;
		}

		public bool SetAudioTrack(MediaPlayEx.AudioTrack audioTrack)
		{
			bool result = false;
			string str = "";
			switch (audioTrack)
			{
			case MediaPlayEx.AudioTrack.H:
				str = "stereo";
				break;
			case MediaPlayEx.AudioTrack.L:
				str = "left";
				break;
			case MediaPlayEx.AudioTrack.R:
				str = "right";
				break;
			}
			if (MediaPlayEx.mciSendString("setaudio  MEDIA source to " + str, null, 0, IntPtr.Zero) == 0)
			{
				result = true;
			}
			return result;
		}

		public bool SetMute(bool AudioOff)
		{
			bool result = false;
			string str;
			if (AudioOff)
			{
				str = "off";
			}
			else
			{
				str = "on";
			}
			if (MediaPlayEx.mciSendString("setaudio MEDIA " + str, null, 0, IntPtr.Zero) == 0)
			{
				result = true;
			}
			this.Mute = AudioOff;
			return result;
		}

		public bool CloseMusic()
		{
			if (MediaPlayEx.mciSendString("close MEDIA", null, 0, IntPtr.Zero) == 0)
			{
				this.Opened = false;
				return true;
			}
			return false;
		}

		public bool PauseMusic()
		{
			return MediaPlayEx.mciSendString("pause MEDIA", null, 0, IntPtr.Zero) == 0;
		}

		public MediaPlayEx.Playstate GetPlayState()
		{
			this.durLength = "";
			this.durLength = this.durLength.PadLeft(128, Convert.ToChar(" "));
			MediaPlayEx.mciSendString("status MEDIA mode", this.durLength, this.durLength.Length, IntPtr.Zero);
			this.durLength = this.durLength.Trim();
			MediaPlayEx.Playstate result;
			if (this.durLength.Substring(0, 7) == "playing" || this.durLength.Substring(0, 2) == "播放")
			{
				result = MediaPlayEx.Playstate.Playing;
			}
			else if (this.durLength.Substring(0, 7) == "stopped" || this.durLength.Substring(0, 2) == "停止")
			{
				result = MediaPlayEx.Playstate.Stopped;
			}
			else
			{
				result = MediaPlayEx.Playstate.Pause;
			}
			return result;
		}

		public int GetMusicPos()
		{
			this.durLength = "";
			this.durLength = this.durLength.PadLeft(128, Convert.ToChar(" "));
			MediaPlayEx.mciSendString("status MEDIA position", this.durLength, this.durLength.Length, IntPtr.Zero);
			this.durLength = this.durLength.Trim();
			if (string.IsNullOrEmpty(this.durLength))
			{
				return 0;
			}
			return (int)Convert.ToDouble(this.durLength);
		}

		public string GetMusicPosString()
		{
			this.durLength = "";
			this.durLength = this.durLength.PadLeft(128, Convert.ToChar(" "));
			MediaPlayEx.mciSendString("status MEDIA position", this.durLength, this.durLength.Length, IntPtr.Zero);
			this.durLength = this.durLength.Trim();
			if (string.IsNullOrEmpty(this.durLength))
			{
				return "00:00:00";
			}
			int num = Convert.ToInt32(this.durLength) / 1000;
			int num2 = num / 3600;
			int num3 = (num - num2 * 3600) / 60;
			num -= num2 * 3600 + num3 * 60;
			return string.Format("{0:D2}:{1:D2}:{2:D2}", num2, num3, num);
		}

		public int GetMusicLength()
		{
			this.durLength = "";
			this.durLength = this.durLength.PadLeft(128, Convert.ToChar(" "));
			MediaPlayEx.mciSendString("status MEDIA length", this.durLength, this.durLength.Length, IntPtr.Zero);
			this.durLength = this.durLength.Trim();
			if (string.IsNullOrEmpty(this.durLength))
			{
				return 0;
			}
			return Convert.ToInt32(this.durLength);
		}

		public string GetMusicLengthString()
		{
			this.durLength = "";
			this.durLength = this.durLength.PadLeft(128, Convert.ToChar(" "));
			MediaPlayEx.mciSendString("status MEDIA length", this.durLength, this.durLength.Length, IntPtr.Zero);
			this.durLength = this.durLength.Trim();
			if (string.IsNullOrEmpty(this.durLength))
			{
				return "00:00:00";
			}
			int num = Convert.ToInt32(this.durLength) / 1000;
			int num2 = num / 3600;
			int num3 = (num - num2 * 3600) / 60;
			num -= num2 * 3600 + num3 * 60;
			return string.Format("{0:D2}:{1:D2}:{2:D2}", num2, num3, num);
		}

		public bool SetMusicPos(int Position)
		{
			string lpstrCommand = string.Format("seek MEDIA to {0}", Position);
			return MediaPlayEx.mciSendString(lpstrCommand, null, 0, IntPtr.Zero) == 0;
		}

		private string GetCurrPath(string name)
		{
			if (name.Length < 1)
			{
				return "";
			}
			name = name.Trim();
			name = name.Substring(0, name.Length - 1);
			return name;
		}
	}
}

