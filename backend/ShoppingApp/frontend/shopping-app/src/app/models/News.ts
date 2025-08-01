export interface News {
  newsId: number;
  userId: number;
  title: string;
  shortDescription: string;
  image: string;
  content: string;
  createdDate: string;
  status: number | null;
}
