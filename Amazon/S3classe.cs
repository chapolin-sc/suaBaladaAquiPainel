using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;

namespace suaBaladaAqui2.Amazon
{
    public class S3classe
    {
        public S3classe(){
        }
        
        private static readonly RegionEndpoint bucketRegiao = RegionEndpoint.USEast1;
        private static IAmazonS3 s3Cliente = new AmazonS3Client(bucketRegiao);

        private string nomeBucket = "";
        private string nomeArquivo = "";
        private string caminhoArquivo = "";
        


        public async Task<bool> CriarBucketAsync(string bucketNome){
            try{
                if(!(await AmazonS3Util.DoesS3BucketExistV2Async(s3Cliente, bucketNome))){
                    var bucketRequest = new PutBucketRequest{
                        BucketName = bucketNome,
                        UseClientRegion = true
                    };

                    PutBucketResponse bucketResponse = await s3Cliente.PutBucketAsync(bucketRequest);
                    return bucketResponse.HttpStatusCode == System.Net.HttpStatusCode.OK ? true : false;
                
                }else{  
                    return true;
                }

            }catch (AmazonS3Exception e)
            {
                Console.WriteLine( e.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine( e.Message);
                return false;
            }
        }
    }

}