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
    public class DamageClaimService
    {
        private readonly Guid _userId;
        public DamageClaimService(Guid userId)
        {_userId = userId;}

        public bool CreateClaim(DamageClaimCreate model)
        {
            var entity =
                new DamageClaim()
                {
                    Description = model.Description,
                    ClaimSubmitted = model.ClaimSubmitted,
                    ClaimNotes = model.ClaimNotes,
                    ClaimResolved = model.ClaimResolved,
                    InventoryId = model.InventoryId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.DamageClaims.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<DamageClaimListItem> GetClaims()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.DamageClaims.Select
                    (
                        e =>
                        new DamageClaimListItem
                        {
                            ClaimId = e.ClaimId,
                            ClaimSubmitted = e.ClaimSubmitted,
                            ClaimResolved = e.ClaimResolved,
                            InventoryId = e.InventoryId
                        });
                return query.ToArray();
            }
        }
        public DamageClaimDetail GetClaimById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.DamageClaims.Single(e => e.ClaimId ==id);
                return
                    new DamageClaimDetail
                    {
                        ClaimId = entity.ClaimId,
                        Description = entity.Description,
                        ClaimSubmitted = entity.ClaimSubmitted,
                        ClaimNotes = entity.ClaimNotes,
                        ClaimResolved = entity.ClaimResolved,
                        InventoryId = entity.InventoryId
                    };
            }
        }
        public bool UpdateClaim(DamageClaimEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.DamageClaims.Single(e => e.ClaimId == model.ClaimId);
                entity.ClaimId = model.ClaimId;
                entity.Description = model.Description;
                entity.ClaimSubmitted = model.ClaimSubmitted;
                entity.ClaimNotes = model.ClaimNotes;
                entity.ClaimResolved = model.ClaimResolved;
                entity.InventoryId = model.InventoryId;
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteClaim(int claimId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.DamageClaims.Single(e => e.ClaimId == claimId);
                ctx.DamageClaims.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
