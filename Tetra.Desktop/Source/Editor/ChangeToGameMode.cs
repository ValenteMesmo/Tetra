using Microsoft.Xna.Framework.Input;

namespace Tetra.Desktop
{
    public class ChangeToGameMode : IHandleUpdates
    {
        private readonly EditorWorld editor;
        private readonly CooldownTracker cooldown;

        public ChangeToGameMode(EditorWorld editor, CooldownTracker cooldown)
        {
            this.editor = editor;
            this.cooldown = cooldown;
            cooldown.Start();
        }

        public void Update()
        {
            cooldown.Update();
            if (Keyboard.GetState().IsKeyDown(Keys.F1) && cooldown.IsOver())
            {
                editor.Play();
                cooldown.Start();
            }
        }
    }

    public class ChangeToEditorMode : IHandleUpdates
    {
        private readonly EditorWorld editor;
        private readonly CooldownTracker cooldown;

        public ChangeToEditorMode(EditorWorld editor, CooldownTracker cooldown)
        {
            this.editor = editor;
            this.cooldown = cooldown;
            cooldown.Start();
        }

        public void Update()
        {
            cooldown.Update();
            if (Keyboard.GetState().IsKeyDown(Keys.F1) && cooldown.IsOver())
            {
                editor.Pause();
                cooldown.Start();
            }
        }
    }
}
