
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TypeInvaders.Classes {
    public static class EnemyManager {

        public static GraphicsDevice graphicsDevice { get; set; }
        public static SpriteFont BasicWordFont { get; set; }
        public static SpriteFont MarkedWordFont{ get; set; }
        public static Texture2D Background { get; set; }
        public static Rectangle GameArea { get; set; }

        public static List<Enemy> enemiesToRemove = new List<Enemy>();

        static List<Enemy> enemies = new List<Enemy>();

        static int banan = 0;

        public static void Update(KeyboardState state) {
            if(banan == 0) {
                AddNewEnemy();
                banan++;
            }

            // Only update the state of words if there's been a keypress
            if (state.GetPressedKeys().Length > 0) {
                if (state.IsKeyDown(Keys.Space)) {
                    ResetAllWords();
                }

                foreach (Enemy enemy in enemies) {
                    enemy.UpdateWordState(state);
                }

                // If any words are done, remove them
                foreach (Enemy enemy in enemiesToRemove) {
                    enemies.Remove(enemy);
                }
                enemiesToRemove.Clear();
            }
        }

        public static void Draw(SpriteBatch spriteBatch) {
            foreach (Enemy enemy in enemies) {
                enemy.Draw(spriteBatch);
            }
        }

        private static void ResetAllWords() {
            foreach (Enemy enemy in enemies) {
                enemy.Reset();
            }
        }

        private static void AddNewEnemy() {
            // Width depending on word length, position that don't overlap
            enemies.Add(new Enemy(graphicsDevice, BasicWordFont, MarkedWordFont, GetRandomWord(), new Rectangle(GameArea.X, GameArea.Y, 100, 100)));
            //enemies.Add(new Enemy(graphicsDevice, BasicWordFont, MarkedWordFont, GetRandomWord(), new Rectangle(200, 200, 100, 100)));
        }

        private static string GetRandomWord() {
            string[] words = { "hejsan", "banan" }; // Maybe load list of words from file
            // Do proper random
            int rand = 0;
            return words[rand];
        }
    }
}
