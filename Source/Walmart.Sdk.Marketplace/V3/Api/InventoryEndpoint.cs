/**
Copyright (c) 2018-present, Walmart Inc.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

namespace Walmart.Sdk.Marketplace.V3.Api
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Walmart.Sdk.Base.Primitive;
	using Walmart.Sdk.Marketplace.V3.Payload.Feed;
	using Walmart.Sdk.Marketplace.V3.Payload.Inventory;

	public class InventoryEndpoint : Base.Primitive.BaseEndpoint
	{
		private FeedEndpoint feedApi;

		public InventoryEndpoint(Base.Primitive.IEndpointClient client) : base(client)
		{
			feedApi = new FeedEndpoint(apiClient);
			payloadFactory = new V3.Payload.PayloadFactory();
		}

		public async Task<Inventory> GetItem(string sku, string shipNode = null)
		{
			// to avoid deadlock if this method is executed synchronously
			await new ContextRemover();

			var request = CreateRequest();

			request.EndpointUri = "/v3/inventory";

			request.QueryParams.Add("sku", sku);
			request.QueryParams.Add("shipNode", shipNode);

			var response = await client.GetAsync(request);
			var result = await ProcessResponse<Inventory>(response);
			return result;
		}

		public async Task<Inventory> UpdateItem(Inventory inventory, string shipNode = null)
		{
			// to avoid deadlock if this method is executed synchronously
			await new ContextRemover();

			var request = CreateRequest();

			request.EndpointUri = "/v3/inventory";
			request.AddPayload(inventory);
			request.QueryParams.Add("sku", inventory.Sku);
			request.QueryParams.Add("shipNode", shipNode);

			var response = await client.PutAsync(request);
			var result = await ProcessResponse<Inventory>(response);
			return result;
		}


		/* proxy call to Feed endpoint */
		public async Task<FeedAcknowledgement> UpdateBulkInventory(System.IO.Stream file)
		{
			return await feedApi.UploadFeed(file, V3.Payload.FeedType.inventory);
		}

		public async Task<FeedAcknowledgement> UpdateBulkInventory(List<Inventory> inventoryItems)
		{
			InventoryFeed inventoryFeed = new InventoryFeed
			{
				InventoryHeader = new InventoryHeader
				{
					FeedDate = DateTime.UtcNow,
					Version = InventoryHeaderVersion.Item14,
				},
				Items = inventoryItems,
			};
			return await feedApi.UploadFeed(inventoryFeed, V3.Payload.FeedType.inventory);
		}
	}
}
