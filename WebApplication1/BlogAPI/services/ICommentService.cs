﻿namespace WebApplication1.services;

public interface IComment
{
    Task <bool> AddComment(Comment comment);
    Task<List<Comment>> PostComments(int PostId);
}