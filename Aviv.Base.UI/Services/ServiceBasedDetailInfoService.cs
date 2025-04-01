using Aviv.Base.UI.Models;

namespace Aviv.Base.UI.Services
{
    public class ServiceBasedDetailInfoService
    {
        // In a real app, this would be backed by a database
        // For this demo, we'll use an in-memory list
        private readonly List<ServiceBasedDetail> _serviceDetails;

        public ServiceBasedDetailInfoService()
        {
            // Initialize with sample data
            _serviceDetails =
            [
                new ServiceBasedDetail
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Service_Name = "Strategic Business Consulting",  // Added Service_Name
                    Type_of_Services_Provided = "Consulting",
                    Average_Service_Turnaround_Time = "2-3 Days",
                    Certification_Filename = "iso9001-certification.pdf",
                    Has_Certifications = true
                },
                new ServiceBasedDetail
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Service_Name = "Express Equipment Repair",  // Added Service_Name
                    Type_of_Services_Provided = "Repair",
                    Average_Service_Turnaround_Time = "24 Hours",
                    Certification_Filename = "technical-certification.pdf",
                    Has_Certifications = true
                },
                new ServiceBasedDetail
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Service_Name = "Professional System Installation",  // Added Service_Name
                    Type_of_Services_Provided = "Installation",
                    Average_Service_Turnaround_Time = "2-3 Days",
                    Certification_Filename = null,
                    Has_Certifications = false
                },
                new ServiceBasedDetail
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Service_Name = "Premium Supply Chain Solutions",  // Added Service_Name
                    Type_of_Services_Provided = "Logistics",
                    Average_Service_Turnaround_Time = "Custom Agreement",
                    Certification_Filename = "logistics-award.pdf",
                    Has_Certifications = true
                },
                new ServiceBasedDetail
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Service_Name = "Rapid Quality Assurance",  // Added Service_Name
                    Type_of_Services_Provided = "Quality Inspection",
                    Average_Service_Turnaround_Time = "24 Hours",
                    Certification_Filename = null,
                    Has_Certifications = false
                }
            ];
        }

        // Get all service details with pagination and optional filtering
        public async Task<IEnumerable<ServiceBasedDetail>> GetServiceDetailsAsync(int skip, int take, string? filter = null)
        {
            // Simulate an async operation
            await Task.Delay(100);

            IEnumerable<ServiceBasedDetail> query = _serviceDetails;

            // Apply filter if provided
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(d =>
                    d.Service_Name.Contains(filter, StringComparison.OrdinalIgnoreCase) || // Added Service_Name
                    d.Type_of_Services_Provided.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                    d.Average_Service_Turnaround_Time.Contains(filter, StringComparison.OrdinalIgnoreCase)
                );
            }

            // Apply paging
            return query.Skip(skip).Take(take);
        }

        // Get count of service details with optional filtering
        public async Task<int> GetServiceDetailsCountAsync(string? filter = null)
        {
            // Simulate an async operation
            await Task.Delay(100);

            if (string.IsNullOrEmpty(filter))
            {
                return _serviceDetails.Count;
            }

            return _serviceDetails.Count(d =>
                d.Service_Name.Contains(filter, StringComparison.OrdinalIgnoreCase) || // Added Service_Name
                d.Type_of_Services_Provided.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                d.Average_Service_Turnaround_Time.Contains(filter, StringComparison.OrdinalIgnoreCase)
            );
        }

        // Get a single service detail by ID
        public async Task<ServiceBasedDetail> GetServiceDetailByIdAsync(Guid id)
        {
            // Simulate an async operation
            await Task.Delay(100);

            return _serviceDetails.FirstOrDefault(d => d.Id == id);
        }

        // Create a new service detail
        public async Task<ServiceBasedDetail> CreateServiceDetailAsync(ServiceBasedDetail detail)
        {
            // Simulate an async operation
            await Task.Delay(100);

            // Ensure we have a new ID
            detail.Id = Guid.NewGuid();

            _serviceDetails.Add(detail);
            return detail;
        }

        // Update an existing service detail
        public async Task<ServiceBasedDetail> UpdateServiceDetailAsync(ServiceBasedDetail detail)
        {
            // Simulate an async operation
            await Task.Delay(100);

            int index = _serviceDetails.FindIndex(d => d.Id == detail.Id);
            if (index == -1)
            {
                throw new Exception("Service detail not found");
            }

            _serviceDetails[index] = detail;
            return detail;
        }

        // Delete a service detail
        public async Task DeleteServiceDetailAsync(Guid id)
        {
            // Simulate an async operation
            await Task.Delay(100);

            ServiceBasedDetail? detail = _serviceDetails.FirstOrDefault(d => d.Id == id);
            if (detail == null)
            {
                throw new Exception("Service detail not found");
            }

            _serviceDetails.Remove(detail);
        }

        // Get service types (would typically come from a database or configuration)
        public async Task<List<string>> GetServiceTypesAsync()
        {
            // Simulate an async operation
            await Task.Delay(100);

            return
            [
                "Consulting",
                "Repair",
                "Installation",
                "Logistics",
                "Warehousing",
                "Quality Inspection",
                "Maintenance",
                "Technical Support",
                "Training",
                "Research & Development"
            ];
        }

        // Get turnaround times (would typically come from a database or configuration)
        public async Task<List<string>> GetTurnaroundTimesAsync()
        {
            // Simulate an async operation
            await Task.Delay(100);

            return
            [
                "24 Hours",
                "2-3 Days",
                "More than 3 Days",
                "Custom Agreement"
            ];
        }
    }
}