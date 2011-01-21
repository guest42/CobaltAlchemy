using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CobaltAlchemy
{
    //The base for all major objects in the game. FObject itself
    //is a static object, that doesn't have any behavior, but can have a
    //texture drawn at a set of coordinates
    class FObject
    {
        public Vector3 position;
        public double radius;
        public int texture_index;
        public bool drawable, collidable;

        public FObject(Vector3 _position) 
        {
            position = _position;
            radius = 1;
            texture_index = 0;
            drawable = false;
            collidable = false;
        }

        public FObject(Vector3 _position, double _radius, int _texture_index, bool _drawable, bool _collidable)
        {
            position = _position;
            radius = _radius;
            texture_index = _texture_index;
            drawable = _drawable;
            collidable = _collidable;
        }

        public void update(double elapsed_time)
        {
        }
    }

    //Players, monsters, and NPCS will be derived from FActor, which describes things a nonstatic
    //object should have
    class FActor : FObject
    {
        public double hit_points, max_hit_points, resource_points, max_resource_points;
        public double max_speed;
        public Vector2 velocity;
        public int current_state, direction;
        public LinkedList<FActorState> states;

        public FActor(Vector3 _position) : base(_position)
        {
            states = new LinkedList<FActorState>();
            states.AddLast(new FActorState(1, 1));
        }

        public void update(double elapsed_time)
        {
            base.update(elapsed_time);
        }
    }

    class FActorState {
        int num_frames, frames_per_second, current_frame;

        public FActorState(int _num_frames, int _frames_per_second)
        {
            current_frame = 0;
            num_frames = _num_frames;
            frames_per_second = _frames_per_second;
        }

    }

    //The 'player' class defines behaviors that allow the actual player
    //to control its actions  
    class FPlayer : FActor
    {
        public FPlayer(Vector3 _position) : base(_position)
        {

        }

        public void update(double elapsed_time)
        {
            base.update(elapsed_time);
        }
    }


}
