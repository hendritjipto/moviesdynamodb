// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX - License - Identifier: Apache - 2.0

namespace HighLevelItemCRUDExample
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;

    // snippet-start:[dynamodb.dotnetv3.HighLevelItemCRUDExample]

    /// <summary>
    /// Shows how to perform high-level CRUD operations on an Amazon DynamoDB
    /// table. The example was created with the AWS SDK for .NET version 3.7
    /// and .NET Core 5.0.
    /// </summary>
    public class HighLevelItemCrud
    {
        public static async Task Main()
        {
            var client = new AmazonDynamoDBClient();
            DynamoDBContext context = new DynamoDBContext(client);


            StreamReader sr = new StreamReader(@"hendritjiptomovies-mongo.json");

            string message = sr.ReadToEnd();
            MovieJson[] moviesArray = MovieJson.FromJson(message);

            Console.WriteLine("length : " + moviesArray.Length);
            sr.Close();
            int counter = 0;

            List<Movie> lMovies = new List<Movie>();

            for (int i = 0; i < moviesArray.Length; i++)
            {
                if (counter < 25)
                {
                    Movie myMovie = new Movie();
                    myMovie.title = moviesArray[i].Title;
                    myMovie.year = moviesArray[i].Year;

                    InfoX theInfo = new InfoX();
                    List<string> lActor = new List<string>();
                    if (moviesArray[i].Info.Actors != null)
                    {
                        foreach (var item in moviesArray[i].Info.Actors)
                        {
                            lActor.Add(item);
                        }
                        theInfo.actors = lActor;
                    }
                    if (moviesArray[i].Info.Directors != null)
                    {
                        theInfo.directors = moviesArray[i].Info.Directors[0];
                    }
                    if (moviesArray[i].Info.ImageUrl != null)
                    {
                        theInfo.image_url = moviesArray[i].Info.ImageUrl.ToString();
                    }
                    theInfo.plot = moviesArray[i].Info.Plot;
                    theInfo.rank = moviesArray[i].Info.Rank;

                    if (moviesArray[i].Info.ReleaseDate != null)
                    {
                        theInfo.releasedate = (int)moviesArray[i].Info.ReleaseDate.ToUnixTimeSeconds();
                    }
                    if (moviesArray[i].Info.RunningTimeSecs != null)
                    {
                        theInfo.running_time_secs = (int)moviesArray[i].Info.RunningTimeSecs;
                    }

                    List<string> lGenres = new List<string>();
                    if (moviesArray[i].Info.Genres != null)
                    {
                        foreach (var item in moviesArray[i].Info.Genres)
                        {
                            lGenres.Add(item.ToString());
                        }
                    }
                    theInfo.genres = lGenres;

                    myMovie.info = theInfo;

                    lMovies.Add(myMovie);
                    counter++;
                }
                else
                {

                    var movieBatch = context.CreateBatchWrite<Movie>();
                    movieBatch.AddPutItems(lMovies);
                    await movieBatch.ExecuteAsync();

                    lMovies = new List<Movie>();
                    counter = 0;
                }
            }

            #region to finsih off the loop 
            var bookBatch = context.CreateBatchWrite<Movie>();
            bookBatch.AddPutItems(lMovies);
            await bookBatch.ExecuteAsync();

            lMovies = new List<Movie>();
            counter = 0;
            #endregion

        }
    }

    // snippet-end:[dynamodb.dotnetv3.HighLevelItemCRUDExample]
}
