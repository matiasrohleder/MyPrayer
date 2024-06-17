﻿using Entities.Models;

namespace WebAPI.DTOs
{
    public class PaginatedContentRes
    {
        public List<ContentRes> Content { get; set; }
        public int TotalPages { get; set; }
        
        public PaginatedContentRes(List<ContentRes> content, int totalPages)
        {
            Content = content;
            TotalPages = totalPages;
        }
    }
}