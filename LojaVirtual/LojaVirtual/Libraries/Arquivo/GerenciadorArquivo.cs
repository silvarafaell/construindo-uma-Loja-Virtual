﻿using LojaVirtual.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Arquivo
{
    public class GerenciadorArquivo
    {
        public static string CadastrarImagemProduto(IFormFile file)
        {
            var NomeArquivo = Path.GetFileName(file.FileName);
            var Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", NomeArquivo);

            using (var stream = new FileStream(Caminho, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return Path.Combine("/uploads/temp", NomeArquivo).Replace("\\","/");
        }

        public static bool ExcluirImagemProduto(string caminho)
        {
            string Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminho.TrimStart('/'));
            if (File.Exists(Caminho))
            {
                File.Delete(Caminho);
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static List<Imagem> MoverImagensProduto(List<string> ListaCaminhoTemp, int ProdutoId)
        {
            //Criar a pasta do produto
            var CaminhoDefinitivoPastaProduto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", ProdutoId.ToString());

            if(!Directory.Exists(CaminhoDefinitivoPastaProduto))
            {
                Directory.CreateDirectory(CaminhoDefinitivoPastaProduto);
            }

            //Mover a Imagem da pasta temp para a pasta definitiva
            List<Imagem> ListaImagensDef = new List<Imagem>();

            foreach(var CaminhoTemp in ListaCaminhoTemp)
            {
                if (!string.IsNullOrEmpty(CaminhoTemp))
                {
                    var NomeArquivo = Path.GetFileName(CaminhoTemp);

                    var CaminhoDef = Path.Combine("uploads", ProdutoId.ToString(), NomeArquivo).Replace("\\", "/");

                    if (CaminhoDef != CaminhoTemp)
                    {
                        var CaminhoAbsolutoTemp = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", NomeArquivo);
                        var CaminhoAbsolutoDef = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", ProdutoId.ToString(), NomeArquivo);

                        if (File.Exists(CaminhoAbsolutoTemp))
                        {
                            //Deleta o arquivo no caminho de destino
                            if(File.Exists(CaminhoAbsolutoDef))
                            {
                                File.Delete(CaminhoAbsolutoDef);
                            }
                            //Copia o arquivo da pasta temporaria para o destino
                            File.Copy(CaminhoAbsolutoTemp, CaminhoAbsolutoDef);

                            //Deleta o arquivo da pasta temporaria
                            if (File.Exists(CaminhoAbsolutoDef))
                            {
                                File.Delete(CaminhoAbsolutoTemp);
                            }

                            ListaImagensDef.Add(new Imagem() { Caminho = Path.Combine("uploads", ProdutoId.ToString(), NomeArquivo).Replace("\\", "/"), ProdutoId = ProdutoId });
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        ListaImagensDef.Add(new Imagem() { Caminho = Path.Combine("uploads", ProdutoId.ToString(), NomeArquivo).Replace("\\", "/"), ProdutoId = ProdutoId });
                    }

                }
            }
            return ListaImagensDef;
        }

        public static void ExcluirImagensProduto(List<Imagem> ListaImagem)
        {
            int ProdutoId = 0;
            foreach(var Imagem in ListaImagem)
            {
                ExcluirImagemProduto(Imagem.Caminho);
                ProdutoId = Imagem.ProdutoId;
            }

            var PastaProduto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", ProdutoId.ToString());

            if(Directory.Exists(PastaProduto))
            {
                Directory.Delete(PastaProduto);
            }
        }
    }
}
