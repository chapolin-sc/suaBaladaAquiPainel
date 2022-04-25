using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;
using Amazon.S3.Util;

namespace suaBaladaAqui2.Amazon
{
    public class S3classe
    {
        public S3classe(){
        }

        /*public S3classe(string nomeBucket, string nomeArquivo, string caminhoArquivo){
            this.nomeBucket = nomeBucket;
            this.nomeArquivo = nomeArquivo;
            this.caminhoArquivo = caminhoArquivo;
        }*/
        /*private string nomeBucket = "";
        private string nomeArquivo = "";
        private string caminhoArquivo = "";   */  
        
        private static readonly RegionEndpoint bucketRegiao = RegionEndpoint.USEast1;
        private static IAmazonS3 s3Cliente = new AmazonS3Client(bucketRegiao);

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

        public async Task<bool> SalvandoArquivosNoBucketS3Async(IFormFile arquivo, string nomeBucket, 
        string nomeArquivo){
            try{
                
                var transferAquivo = new TransferUtility(s3Cliente);
                using(MemoryStream ms = new MemoryStream()){
                    await arquivo.OpenReadStream().CopyToAsync(ms);
                    await transferAquivo.UploadAsync(ms, nomeBucket, nomeArquivo);
                }

                return true;

            }catch(AmazonS3Exception e){
                Console.WriteLine(e.Message);
                return false;
            }catch(Exception  e){
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }
}