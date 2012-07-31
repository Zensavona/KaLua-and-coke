using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace Emulator.ClassMaps
{
    public class Npc 
    {
        private int npcid;
        private int hidden;
        private int kind;
        private int shape;
        private int talk;
        private int x;
        private int y;
        private int z;
        private int hx;
        private int hy;
        private uint worldId;

        public Npc() {
            worldId = World.NewId();
        }
        
        public virtual sbyte DX {
            get { 
                int dx = hx - x;
                int dy = hy - y;
                
                double scale = 127.0 / Math.Max(Math.Abs(dx), Math.Abs(dy));
                return (sbyte)Math.Round(dx * scale);    
            }
        }
        
        public virtual sbyte DY {
            get { 
                int dx = hx - x;
                int dy = hy - y;
                
                double scale = 127.0 / Math.Max(Math.Abs(dx), Math.Abs(dy));
                return (sbyte)Math.Round(dy * scale);    
            }
        }		
        
        public virtual int NpcId {
            get { return npcid; }
            set { npcid = value; }
        }

        public virtual int Kind {
            get { return kind; }
            set { kind = value; }
        }

        public virtual int Shape {
            get { return shape; }
            set { shape = value; }
        }

        public virtual int Talk {
            get { return talk; }
            set { talk = value; }
        }
        
        public virtual int Hidden {
            get { return hidden; }
            set { hidden = value; }
        }
        
        public virtual bool IsHidden {
            get { return (hidden == 1); }
            set { hidden = (value ? 1 : 0); }
        }

        public virtual int X {
            get { return x; }
            set { x = value; }
        }

        public virtual int Y {
            get { return y; }
            set { y = value; }
        }

        public virtual int Z {
            get { return z; }
            set { z = value; }
        }

        public virtual int HX {
            get { return hx; }
            set { hx = value; }
        }

        public virtual int HY {
            get { return hy; }
            set { hy = value; }
        }
        
        public virtual uint WorldId {
            get { return worldId; }
            set { worldId = value; }
        }
        
        public static Npc Get(int npcId) 
        {
            Npc npc;
            using(ISession session = Server.Factory.OpenSession())
            {
                IQuery q = session.CreateQuery("FROM Npc WHERE NpcId = :npcId");
                       q.SetParameter("npcId",npcId);
                npc = q.UniqueResult<Npc>();
            }
            return npc;
        }
    }
}