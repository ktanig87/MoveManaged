using MoveManaged.Data;
using MoveManaged.Models;
using MoveManaged.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveManaged.Services
{
    public class RoomService
    {
         
        private readonly Guid _userId;
        public RoomService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateMove(RoomCreate model)
        {
            var entity =
                new Room()
                {
                    RoomName = model.RoomName,
                    MoveId = model.MoveId

                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Rooms.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<RoomListItem> GetRooms()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Rooms
                    .Select(
                        e =>
                        new RoomListItem
                        {
                            RoomId = e.RoomId,
                            RoomName = e.RoomName
                        }
                        );
                return query.ToArray();
            }
        }

        public RoomDetail GetRoombyId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Rooms
                    .Single(e => e.RoomId == id);
                return
                    new RoomDetail
                    {
                        RoomId = entity.RoomId,
                        RoomName = entity.RoomName,
                        MoveId = entity.MoveId
                    };

            }

        }

        public bool UpdateRoom(RoomEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Rooms
                    .Single(e => e.RoomId == model.RoomId);
                entity.RoomId = model.RoomId;
                entity.RoomName = model.RoomName;
                entity.MoveId = model.MoveId;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Rooms
                    .Single(e => e.RoomId == roomId);
                ctx.Rooms.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}

