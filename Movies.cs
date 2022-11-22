// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX - License - Identifier: Apache - 2.0

namespace HighLevelItemCRUDExample
{
    using System;
    using System.Collections.Generic;
    using Amazon.DynamoDBv2.DataModel;

    // snippet-start:[dynamodb.dotnet35.HighLevelItemCRUD.Book]

    [DynamoDBTable("hendritjiptomovies")]
    public class Movie
    {
        [DynamoDBHashKey] // Partition key
        public string title { get; set; }

        [DynamoDBRangeKey] // Sort key
        public int year { get; set; }


        // String Set datatype
        public InfoX info { get; set; }


    }

    public class InfoX
    {

        public List<string> actors { get; set; }


        public List<string> genres { get; set; }


        public string directors { get; set; }


        public string image_url { get; set; }

        public string plot { get; set; }

        public int rank { get; set; }


        public int releasedate { get; set; }

        public int running_time_secs { get; set; }
    }
    // snippet-end:[dynamodb.dotnet35.HighLevelItemCRUD.Book]
}
