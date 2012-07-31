using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.Maps
{
    public abstract class WorldObject
    {
        /// <summary>
        /// Object's ID in the world.
        /// </summary>
        protected uint _worldID;

        /// <summary>
        /// Get's the world ID of the object.
        /// </summary>
        public uint WorldID
        {
            get
            {
                return _worldID;
            }
        }

        /// <summary>
        /// Position on the X axis.
        /// </summary>
        protected int _x;

        /// <summary>
        /// Position on the Y axis.
        /// </summary>
        protected int _y;

        /// <summary>
        /// Gets or sets the position on the X axis.
        /// </summary>
        public int PosX  {
            get {
                return _x;
            }
            set {
                _y = value;
            }
        }

        /// <summary>
        /// Gets or sets the position on the Y axis.
        /// </summary>
        public int PosY {
            get {
                return _y;
            }
            set{
                _y = value;
            }
        }
    }

    public abstract class WorldPlayer : WorldObject
    {
        /// <summary>
        /// Position on the Z axis.
        /// </summary>
        protected int _z;

        /// <summary>
        /// Gets or sets the position on the Z axis.
        /// </summary>
        public int PosZ{
            get {
                return _z;
            }
            set {
                _z = value;
            }
        }
    }

    public abstract class WorldPerson : WorldPlayer
    {
        /// <summary>
        /// Facing direction on the X axis.
        /// </summary>
        protected int _dx;

        /// <summary>
        /// Facing direction on the Y axis.
        /// </summary>
        protected int _dy;

        /// <summary>
        /// Gets or sets the facing position on the X axis.
        /// </summary>
        public int DirX
        {
            get
            {
                return _x;
            }
            set
            {
                _y = value;
            }
        }

        /// <summary>
        /// Gets or sets the facing position on the Y axis.
        /// </summary>
        public int DirY
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }
    }

}
