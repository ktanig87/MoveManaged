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
    public class BoxService
    {
        private readonly Guid _userId;
        public BoxService(Guid userId)
        { _userId = userId; }

        public bool CreateBox(BoxCreate model)
        {
                var entity =
                    new Box()
                    {
                        BoxSize = model.BoxSize
                    };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Boxes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
            
        }

        public IEnumerable<Box> GetBoxes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Boxes.Select
                    (e =>
                    new Box
                    {
                        BoxId = e.BoxId,
                        RoomId = e.RoomId

                    });
                return query.ToArray();
            }
        }

        public BoxDetail GetBoxId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Boxes.Single(e => e.BoxId == id);
                return
                    new BoxDetail
                    {
                        BoxSize = entity.BoxSize,
                        MoveId = entity.MoveId,
                        RoomId = entity.RoomId
                    };
            }
        }


        public bool UpdateBox(BoxEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Boxes
                    .Single(e => e.BoxId == model.BoxId);
                entity.BoxSize = model.BoxSize;
                return ctx.SaveChanges() == 1;                   
            }

        }

        public bool DeleteBox(int boxId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Boxes.Single(e => e.BoxId == boxId);
                ctx.Boxes.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
