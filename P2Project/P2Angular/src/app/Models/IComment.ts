export interface IComment{
    CommentId: string,
    CommentPostId: number,
    CommentUserId: string,
    UserName: string,
    Timestamp: Date,
    Content: string,
}