﻿namespace Dal.Entities
{
    public class Operation
    {
        public Guid RecipeId { get; set; }

        public int Step {  get; set; }

        public string Description { get; set; }

        public Guid? FileId { get; set; }

        public int TimeInSeconds { get; set; }

        public Recipe Recipe { get; set; }

        public File File { get; set; }
    }
}
