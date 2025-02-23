using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class CProjectServicesEntity
{
    /*
  If you need to query or manage the link table directly (for audit trails,
  filtering, or joining on additional data), then creating a dedicated domain model,
  persistence entity, repository, and factory—as described in the earlier examples—is
  the approach taken in large companies.

  In summary, real-world large companies typically avoid querying the join table directly
  unless there is a justified business need.
  They start by leveraging ORMs with implicit joins to keep designs simple and only
  “promote” the join table to a first‑class entity when additional functionality
  or querying capability is necessary.

/*
    public int ProjectsEntityId { get; set; }
    public int ServicesEntityId { get; set; }
*/
    /*
    [Key, Column(Order = 0)]
    public int ProjectsEntityId { get; set; }
    [Key, Column(Order = 1)]
    public int ServicesEntityId { get; set; }
    */
}