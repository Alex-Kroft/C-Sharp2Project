using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

using MonoGame.Extended.Content;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;
using AgeOfEmpires.Systems;

namespace AgeOfEmpires.Components
{
    class Skin
    {
        public AnimatedSprite unit;
        public String animationName;

        public AnimatedSprite Villager { get; set; }

        public Skin(ContentManager content, String animation, String character) {
            var spriteSheet = content.Load<SpriteSheet>(character, new JsonContentLoader());
            var sprite = new AnimatedSprite(spriteSheet);
            animationName = animation;
            unit = sprite;
        }
    }
}
