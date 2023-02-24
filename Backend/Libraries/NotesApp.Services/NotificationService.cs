﻿using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotesApp.Domain.Abstractions;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Dtos;
using NotesApp.Services.Abstractions;

namespace NotesApp.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> _repository;
        private readonly ILogger<NotificationService> _logger;
        private readonly IMapper _mapper;

        public NotificationService(IRepository<Notification> repository, ILogger<NotificationService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> CreateNotificationAsync(NotificationCreateDto notificationCreateDto, CancellationToken cancellationToken = default)
        {
            return await _repository.CreateAsync(_mapper.Map<Notification>(notificationCreateDto), cancellationToken);
        }

        public async Task<bool> DeleteNotificationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Попытка удаления несуществующего напоминания");

                return false;
            }

            await _repository.DeleteAsync(entity, cancellationToken);

            return true;
        }

        public IQueryable<NotificationViewDto> GetQueryable()
        {
            var entities = _repository.GetQueryable();

            return entities.UseAsDataSource(_mapper.ConfigurationProvider).For<NotificationViewDto>();
        }

        public async Task<bool> UpdateNotificationAsync(NotificationUpdateDto notificationUpdateDto, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(notificationUpdateDto.Id, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Попытка изменения несуществующего напоминания, запрашиваемый идентификатор: {NotificationId}.", notificationUpdateDto.Id);

                return false;
            }

            var changedEntity = _mapper.Map<Notification>(notificationUpdateDto);
            changedEntity.NoteId = entity.NoteId;

            return await _repository.UpdateAsync(changedEntity, cancellationToken);
        }
    }
}