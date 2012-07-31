using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.ClassMaps 
{
    public class DeletedPlayer 
    {
        private int deleteId;
        private int playerId;
        private int userId;
        private DateTime deletedDate;
        
        public DeletedPlayer() {}
        
        public virtual int DeleteId {
            get { return deleteId; }
            set { deleteId = value; }
        }
        
        public virtual int PlayerId {
            get { return playerId; }
            set { playerId = value; }
        }
        
        public virtual int UserId {
            get { return userId; }
            set { userId = value; }
        }
        
        public virtual DateTime DeletedDate {
            get { return deletedDate; }
            set { deletedDate = value; }
        }
        
        private Player player;
        
        public virtual Player Player {
            get { return player;  }
            set { player = value; }            
        }
    }
}
