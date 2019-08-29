using Microsoft.Xna.Framework.Input;

namespace Tetra.Desktop
{
    public class ChangeToGameMode : IHandleUpdates
    {
        private readonly EditorWorld editor;

        public ChangeToGameMode(EditorWorld editor)
        {
            this.editor = editor;
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F1) )
            {
                editor.Play();
            }
        }
    }

    public class ChangeToEditorMode : IHandleUpdates
    {
        private readonly EditorWorld editor;

        public ChangeToEditorMode(EditorWorld editor)
        {
            this.editor = editor;
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                editor.Pause();
            }
        }
    }
}
