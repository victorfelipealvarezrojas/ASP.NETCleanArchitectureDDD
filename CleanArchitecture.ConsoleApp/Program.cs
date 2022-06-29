
using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();


Streamer streamer = new()
{
  Nombre = "Amazon Prime",
  Url = "http://amazonprime.com"
};

//await queryMethodsLambdaExpression();
// await SaveStreamers(streamer);
// await saveVideoStreamer();
await multipleEntityQuery();

Console.WriteLine("Presione cualquier tecla para cerrar el programa");
Console.ReadLine();

async Task multipleEntityQuery()
{

  //var videoWithActors = await dbContext!.Videos!.Include(q => q.Actores).FirstOrDefaultAsync(q => q.Id == 1);

  //var videoWithActorsTwo = await dbContext!.Actores!.Select(q => q.Nombre).ToListAsync();

  var videoWithActors = await dbContext!.Videos!
                                        .Where(p => p.Director != null)
                                        .Include(q => q.Director)
                                        .Select(q =>
                                        new
                                        {
                                          DirectorNombre = $"{q.Director.Nombre} {q.Director.Apellido}",
                                          Movie = q.Nombre
                                        }).ToListAsync();

  foreach (var movie in videoWithActors)
  {
    Console.WriteLine($"{movie.Movie} {movie.DirectorNombre}");
  }

}

async Task addNewActorWithVideoId()
{


  var Actor = new Actor
  {
    Nombre = "Victor",
    Apellido = "Pitt",
    
  };

  await dbContext.AddAsync(Actor); // aqui toma el Id
  await dbContext.SaveChangesAsync();

  var VideoActor = new VideoActor
  {
    ActorId = Actor.Id,
    VideoId = 1
  };



  await dbContext.AddAsync(VideoActor);
  await dbContext.SaveChangesAsync();

}

async Task addNewStreamerNewVideoId()
{
 

  var BatmamForever = new Video
  {
    Nombre = "Batman Forever",
    StreamerId = 2002
  };

  await dbContext.AddAsync(BatmamForever);
  await dbContext.SaveChangesAsync();

}

async Task addNewStreamerNewVideo()
{
  var pantaya = new Streamer
  {
    Nombre = "Pantaya"
  };

  var hungerGames = new Video
  {
    Nombre = "Hunger Games",
    Streamer = pantaya,
  };

  await dbContext.AddAsync(hungerGames);
  await dbContext.SaveChangesAsync();

}

async Task TrackindNotTracking()
{
  var streaminWhiteTracking = await dbContext.Streamers!.FirstOrDefaultAsync(x => x.Id == 1);
  var streaminWhiteNoTracking = await dbContext.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 1002); //libera el objeto de la memoria temporal

  streaminWhiteTracking.Nombre = "Netflix Editado";
  streaminWhiteNoTracking.Nombre = "Amazon Prime Editado"; // no se actualizara

  await dbContext.SaveChangesAsync();

}

async Task queryMethodsLinQ()
{

  Console.WriteLine($"Ingrese una compañia de streaming");
  var sreamingNombre = Console.ReadLine();

  var streamers = await (from i in dbContext.Streamers select i).ToListAsync();
  var streamers2 = await (from i in dbContext.Streamers where EF.Functions.Like(i.Nombre!, $"{sreamingNombre}") select i).ToListAsync();

  foreach (var streamer in streamers)
  {
    Console.WriteLine(streamer.Nombre);
  }
}

async Task queryMethodsLambdaExpression()
{

  var streamer = dbContext!.Streamers!;

  var streamer1 = await streamer.Where(y => y.Nombre!.Contains("a")).FirstAsync();
  var streamer2 = await streamer.Where(y => y.Nombre!.Contains("a")).FirstOrDefaultAsync();
  var streamer3 = await streamer.FirstOrDefaultAsync(y => y.Nombre!.Contains("a"));
  var streamer5 = await streamer.Where(y => y.Nombre!.Contains("a")).SingleOrDefaultAsync();
  var streamer6 = await streamer.FindAsync(1); //Busca por Id

}

async Task QueryFilter()
{
  Console.WriteLine($"Ingrese una compañia de streaming");
  var sreamingNombre = Console.ReadLine();

  var streamers = await dbContext.Streamers!.Where(x => x.Nombre!.Equals(sreamingNombre)).ToListAsync();

  foreach (var streamer in streamers)
  {
    Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
  }

  //var streamerPartialResults = await dbContext.Streamers!.Where(x => x.Nombre!.Contains(sreamingNombre!)).ToListAsync();
  var streamerPartialResults = await dbContext.Streamers!.Where(x => EF.Functions.Like(x.Nombre!,$"%{sreamingNombre}%")).ToListAsync();

  foreach (var streamer in streamerPartialResults)
  {
    Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
  }
}

async Task SaveStreamers(Streamer streamer)
{
  dbContext!.Streamers!.Add(streamer);
  await dbContext.SaveChangesAsync();
}

async Task saveVideoStreamer()
{
  var movies = new List<Video>
{
  new Video { Nombre = "Mad Maz", StreamerId = streamer.Id },
  new Video { Nombre = "Batman", StreamerId = streamer.Id },
  new Video { Nombre = "Dragon Ball Z", StreamerId = streamer.Id },
  new Video { Nombre = "Naruto Choco Ships ", StreamerId = streamer.Id }
};

  await dbContext!.Videos!.AddRangeAsync(movies);
  await dbContext.SaveChangesAsync();
}

