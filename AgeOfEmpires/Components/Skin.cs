using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

using MonoGame.Extended.Content;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;

namespace AgeOfEmpires.Components
{
    class Skin
    {
        public AnimatedSprite villager;
        //OrthographicCamera _camera;

        public Skin(ContentManager content) {
            //_camera = camera;
            var spriteSheet = content.Load<SpriteSheet>("LightBandit.sf", new JsonContentLoader());
            var sprite = new AnimatedSprite(spriteSheet);
            sprite.Play("idle");
            villager = sprite;
        }
    }
}
