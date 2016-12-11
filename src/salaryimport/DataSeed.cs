using Newtonsoft.Json;
using SalaryImport.Model;

namespace SalaryImport
{
    public class DataSeed
  {
    public Person[] Persons =>
      JsonConvert
      .DeserializeObject<Person[]>(PersonsJson);
    public Company[] Companies =>
      JsonConvert
      .DeserializeObject<Company[]>(CompaniesJson);
    public SalaryComponent[] SalaryComponents =>
      JsonConvert
      .DeserializeObject<SalaryComponent[]>(SalaryComponentsJson);
    public Employee[] Employees =>
      JsonConvert
      .DeserializeObject<Employee[]>(EmployeesJson);

    const string PersonsJson = @"[{
""Name"": ""Anna"",
""PersonalIdNumber"": ""720220-1111"",
""Id"": ""13cf4bc2-92a3-4700-b697-fc9fbffe7143""
},
{
""Name"": ""Bertil"",
""PersonalIdNumber"": ""820923-2222"",
""Id"": ""048f4ad0-a6fd-4119-b965-56113ac516de""
},
{
""Name"": ""Cecilia"",
""PersonalIdNumber"": ""620405-3333"",
""Id"": ""107f7ab5-a613-4e10-8538-bc4eb0e75ebc""
},
{
""Name"": ""David"",
""PersonalIdNumber"": ""900515-4444"",
""Id"": ""4734e921-974e-413a-ab03-57b647a5537b""
},
{
""Name"": ""Emma"",
""PersonalIdNumber"": ""431123-5555"",
""Id"": ""3304c52b-267d-4190-81ec-07230061f026""
}]
";
    const string CompaniesJson = @"[{
""LegalName"": ""Testföretaget"",
""OrganizationNumber"": ""551111-1111"",
""Id"": ""4a654fb7-1cc9-4e37-9625-6a24e875c186""
},
{
""LegalName"": ""ITbolaget"",
""OrganizationNumber"": ""552222-2222"",
""Id"": ""aab20a43-b845-452f-8320-6b0c02451e53""
}]";
    const string SalaryComponentsJson = @"[{
""Type"": ""1"",
""Amount"": 32000.0,
""FromDate"": ""2014-03-01T00:00:00"",
""Id"": ""d6b51bfc-dfe6-46f1-b0b4-ff5da815961a""
},
{
""Type"": ""1"",
""Amount"": 40000.0,
""FromDate"": ""2012-05-01T00:00:00"",
""Id"": ""19d039e0-d327-4619-b450-cda1f025f8b9""
},
{
""Type"": ""1"",
""Amount"": 51000.0,
""FromDate"": ""2015-07-01T00:00:00"",
""Id"": ""b4524ab6-a025-4385-95c5-0eb874a42cf5""
},
{
""Type"": ""1"",
""Amount"": 28000.0,
""FromDate"": ""2014-03-15T00:00:00"",
""Id"": ""14b7945d-47b2-4bda-a9b8-9f58d558894d""
},
{
""Type"": ""1"",
""Amount"": 41000.0,
""FromDate"": ""2011-10-12T00:00:00"",
""Id"": ""b45c76ce-3f80-44ab-8270-297e0a089326""
},
{
""Type"": ""2"",
""Amount"": 30000.0,
""FromDate"": ""2014-07-01T00:00:00"",
""Id"": ""80e4f11f-81f1-4cf9-b839-e3af931b78a7""
},
{
""Type"": ""2"",
""Amount"": 40000.0,
""FromDate"": ""2016-03-01T00:00:00"",
""Id"": ""762e89d9-e638-407e-bc06-dae94051e623""
},
{
""Type"": ""2"",
""Amount"": 37000.0,
""FromDate"": ""2015-08-01T00:00:00"",
""Id"": ""f0b42da6-7d29-4587-ad09-7b79fc4972e8""
},
{
""Type"": ""3"",
""Amount"": 8000.0,
""FromDate"": ""2014-03-01T00:00:00"",
""Id"": ""976a55c1-4cae-48e5-b75e-21be23f38716""
},
{
""Type"": ""3"",
""Amount"": 4000.0,
""FromDate"": ""2014-09-17T00:00:00"",
""Id"": ""d0c64bd2-a785-4416-8f4b-144e6c2d136a""
}]";
    const string EmployeesJson = @"[{
""Type"": ""A"",
""PersonId"": ""13cf4bc2-92a3-4700-b697-fc9fbffe7143"",
""CompanyId"": ""4a654fb7-1cc9-4e37-9625-6a24e875c186"",
""StartDate"": ""2005-03-01T00:00:00"",
""SalaryComponentIds"": [""d6b51bfc-dfe6-46f1-b0b4-ff5da815961a""],
""Id"": ""e557f13b-a2f7-4e19-ac10-61501466f8a7""
},
{
""Type"": ""A"",
""PersonId"": ""048f4ad0-a6fd-4119-b965-56113ac516de"",
""CompanyId"": ""4a654fb7-1cc9-4e37-9625-6a24e875c186"",
""StartDate"": ""2011-06-07T00:00:00"",
""SalaryComponentIds"": [""19d039e0-d327-4619-b450-cda1f025f8b9""],
""Id"": ""914b8f33-3d8b-4d88-9328-09d1dc075791""
},
{
""Type"": ""B"",
""PersonId"": ""107f7ab5-a613-4e10-8538-bc4eb0e75ebc"",
""CompanyId"": ""4a654fb7-1cc9-4e37-9625-6a24e875c186"",
""StartDate"": ""1995-07-03T00:00:00"",
""SalaryComponentIds"": [""b4524ab6-a025-4385-95c5-0eb874a42cf5"",
""80e4f11f-81f1-4cf9-b839-e3af931b78a7"",
""d0c64bd2-a785-4416-8f4b-144e6c2d136a""],
""Id"": ""f0cbba78-7bfc-4c12-ac69-540f5a3bcb2d""
},
{
""Type"": ""B"",
""PersonId"": ""4734e921-974e-413a-ab03-57b647a5537b"",
""CompanyId"": ""aab20a43-b845-452f-8320-6b0c02451e53"",
""StartDate"": ""2015-01-15T00:00:00"",
""SalaryComponentIds"": [""14b7945d-47b2-4bda-a9b8-9f58d558894d"",
""762e89d9-e638-407e-bc06-dae94051e623"",
""f0b42da6-7d29-4587-ad09-7b79fc4972e8"",
""976a55c1-4cae-48e5-b75e-21be23f38716""],
""Id"": ""795a2d21-8297-4030-ba84-e0be9e5e85bf""
},
{
""Type"": ""A"",
""PersonId"": ""3304c52b-267d-4190-81ec-07230061f026"",
""CompanyId"": ""aab20a43-b845-452f-8320-6b0c02451e53"",
""StartDate"": ""1993-11-20T00:00:00"",
""SalaryComponentIds"": [""b45c76ce-3f80-44ab-8270-297e0a089326""],
""Id"": ""5c36f079-8385-4535-b2d1-af429eaf77e2""
}]";
  }
}