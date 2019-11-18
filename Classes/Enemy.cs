
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TypeInvaders.Classes {
    public class Enemy {

        GraphicsDevice graphicsDevice;
        RenderTarget2D renderTarget;
        SpriteFont basicWordFont;
        SpriteFont markedWordFont;

        // Maybe change position for Rectangle instead of Vector
        private Rectangle renderTargetFrame;

        // Color settings
        private Color color;
        private Color markedColor;

        // Word contents
        private string originalWord;
        private string finishedLetters;
        private string remainingLetters;

        public Enemy(GraphicsDevice graphicsDevice, SpriteFont basicWordFont, SpriteFont markedWordFont, string word, Rectangle renderTargetFrame) {
            this.basicWordFont = basicWordFont;
            this.markedWordFont = markedWordFont;
            this.graphicsDevice = graphicsDevice;
            this.renderTargetFrame = renderTargetFrame;

            renderTarget = new RenderTarget2D(this.graphicsDevice, this.renderTargetFrame.Width, this.renderTargetFrame.Height);

            color = Color.Gray;
            markedColor = Color.White;

            originalWord = word;
            remainingLetters = originalWord;
        }

        private void UpdateRenderTarget(SpriteBatch spriteBatch) {
            int characterOffset = 0;
            var stringPosition = new Vector2(renderTargetFrame.X, renderTargetFrame.Y);
            
            graphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.Draw(EnemyManager.Background, renderTargetFrame, Color.White);

            // Check if we have any letters already finnished
            if (finishedLetters != null) {
                spriteBatch.DrawString(markedWordFont, finishedLetters, stringPosition, markedColor);
                characterOffset = finishedLetters.Length;
            }

            // Draw the remaining letters with a well balanced offset
            spriteBatch.DrawString(basicWordFont, remainingLetters, new Vector2(stringPosition.X + 10 * characterOffset, stringPosition.Y), color);
            graphicsDevice.SetRenderTarget(null);
        }

        public void UpdateWordState(KeyboardState state) {
            Keys[] keyArray = state.GetPressedKeys();
            string pressedKey = keyArray[0].ToString().ToLower();

            if (remainingLetters.StartsWith(pressedKey)) {
                string[] strList = Regex.Split(remainingLetters, $@"(?<=[{pressedKey}])");
                finishedLetters += strList[0];
                remainingLetters = strList[1];

                //Debug.WriteLine($"Remaining letters are: {remainingLetters}");
            }

            // If the word is done, remove the enemy
            if (remainingLetters.Length == 0) {
                renderTarget.Dispose();
                EnemyManager.enemiesToRemove.Add(this);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            UpdateRenderTarget(spriteBatch);
            spriteBatch.Draw(renderTarget, new Vector2(100, 100), Color.White);
        }

        /// <summary>
        /// Resets the word to it's original state
        /// </summary>
        public void Reset() {
            remainingLetters = originalWord;
            finishedLetters = null;
        }
    }
}
