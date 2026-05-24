import mongoose from "mongoose";

export const connectMongo = async () => {
  if (!process.env.MONGODB_URI) return;
  await mongoose.connect(process.env.MONGODB_URI);
  console.log('connected to mongo db');
}