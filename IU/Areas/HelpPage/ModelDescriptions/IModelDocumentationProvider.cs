using System;
using System.Reflection;

namespace AlphaNet.PassagemAerea.IU.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}