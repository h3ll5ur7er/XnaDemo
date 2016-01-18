using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StandaloneTestScene.GUI
{
	public interface IGUIElement
	{
		void Update ();
		void Render (SpriteBatch batch);
	}

	public abstract class GUIElement : IGUIElement
	{
		public abstract void Update ();
		public abstract void Render (SpriteBatch batch);
	}

	public class Button : GUIElement
	{
		public event StandaloneTestScene.GUI.Events.ClickHandler OnClick;

		public override void Update ()
		{
		}
		public override void Render (SpriteBatch batch)
		{
		}
	}

	public class TextBox : GUIElement
	{
		public override void Update ()
		{
		}
		public override void Render (SpriteBatch batch)
		{
		}
	}


	public class Label : GUIElement
	{
		public override void Update ()
		{
		}
		public override void Render (SpriteBatch batch)
		{
		}
	}
	public class GUIElemenetPanel : GUIElement
	{
		public override void Update ()
		{
		}
		public override void Render (SpriteBatch batch)
		{
		}
	}

}
namespace StandaloneTestScene.GUI.Events
{
	public enum MouseButtons
	{
		Left, Middle, Right
	}

	public delegate void ClickHandler(MouseButtons btn);
	public delegate void Notification();
}