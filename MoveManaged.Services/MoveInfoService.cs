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
    public class MoveInfoService
    {
        private readonly Guid _userId;
        public MoveInfoService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateMove(MoveCreate model)
        {
            var entity =
                new MoveInfo()
                {
                    OwnerId = _userId,
                    MoverName = model.MoverName,
                    DriverPhone = model.DriverPhone,
                    PickupDate = model.PickupDate,
                    DeliveryDate = model.DeliveryDate,
                    TSPPhone = model.TSPPhone
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Moves.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MoveListItem> GetMoves()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Moves.Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new MoveListItem
                        {
                            Id = e.Id,
                            MoverName = e.MoverName
                        }
                        );
                return query.ToArray();
            }
        }

        public MoveDetail GetMovebyId(int id)
        {
                using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Moves
                    .Single(e => e.Id == id && e.OwnerId == _userId);
                return
                    new MoveDetail
                    {
                        Id = entity.Id,
                        MoverName = entity.MoverName,
                        DriverPhone = entity.DriverPhone,
                        PickupDate = entity.PickupDate,
                        DeliveryDate = entity.DeliveryDate,
                        TSPPhone = entity.TSPPhone
                    };

            }

        }
        
        public bool UpdateMove(MoveEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Moves
                    .Single(e => e.Id == model.Id && e.OwnerId == _userId);
                entity.MoverName = model.MoverName;
                entity.DriverPhone = model.DriverPhone;
                entity.PickupDate = model.PickupDate;
                entity.DeliveryDate = model.DeliveryDate;
                entity.TSPPhone = model.TSPPhone;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMove(int moveId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Moves
                    .Single(e => e.Id == moveId && e.OwnerId == _userId);
                ctx.Moves.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
