using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace EFApplicationMF.Method
{
    class ArtistSetController
    {
        MusicContainer dbContext;

        public ArtistSetController()
        {
            dbContext = new MusicContainer();            
        }

        public IList<Artist> GetArtists()
        {
            dbContext.ArtistSet.Load();
            return dbContext.ArtistSet.Local.ToBindingList();
        }

        public void DeleteArtist(Artist artist)
        {
            Artist _artist = dbContext.ArtistSet.Find(artist.Id);
            dbContext.ArtistSet.Remove(_artist);
            dbContext.SaveChanges();
        }

        public void InsertArtist(Artist artist)
        {
            dbContext.ArtistSet.Add(artist);
            dbContext.SaveChanges();
        }

        public void UpdateArtist(Artist artist)
        {
            Artist _artist = dbContext.ArtistSet.Find(artist.Id);

            _artist.Name = artist.Name;
            _artist.Description = artist.Description;
            
            dbContext.SaveChanges();
        }
    }
}
